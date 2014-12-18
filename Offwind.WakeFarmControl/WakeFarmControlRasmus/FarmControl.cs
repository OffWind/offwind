using System;
using System.Linq;
using ILNumerics;

namespace WakeFarmControlR
{
    public static class ILArrayExtensions
    {
        public static int length(this ILArray<double> ilArray)
        {
            return ilArray.Size.Longest;
        }

        public static int length(this ILArray<int> ilArray)
        {
            return ilArray.Size.Longest;
        }

        public static double[][] ToDoubleArray(this ILArray<double> ilArray)
        {
            if (ilArray.Size.ToIntArray().Length != 2)
            {
                throw new ArgumentException();
            }

            var doubleArray = new double[ilArray.Size[0]][];
            for (int i = 0; i <= doubleArray.GetLength(0) - 1; i++)
            {
                doubleArray[i] = new double[ilArray.Size[1]];
                for (int j = 0; j <= doubleArray[i].GetLength(0) - 1; j++)
                {
                    doubleArray[i][j] = ilArray.GetValue(i, j);
                }
            }

            return doubleArray;
        }

        public static void SetRows(this ILArray<double> ilArray, ILArray<double> value, int fromRow, int toRow)
        {
            if (ilArray == null)
            {
                throw new ArgumentException();
            }

            if (value == null)
            {
                throw new ArgumentException();
            }

            if (toRow > ilArray.Size[0] - 1)
            {
                throw new ArgumentException();
            }

            if (value.Size[0] != (toRow - fromRow + 1))
            {
                throw new ArgumentException();
            }

            if (value.Size[1] != ilArray.Size[1])
            {
                throw new ArgumentException();
            }

            for (int index = 0; index <= toRow - fromRow; index++)
            {
                ilArray[fromRow + index, ILMath.full] = value[index, ILMath.full];
            }
        }

        public static ILArray<double> sortrows(this ILArray<double> ilArray, int column)
        {
            ILArray<double> columnValues = ilArray[ILMath.full, column];
            ILArray<int> rowsIndices = ILMath.empty<int>();
            ILArray<double> sortedColumnValues = ILMath.sort(columnValues, rowsIndices);

            //ILArray<double> sortedArray = ilArray[ILMath.full, 0][rowsIndices];
            ILArray<double> sortedArray = ILMath.empty(ilArray.Size[0], 0);
            for (var columnIndex = 0; columnIndex < ilArray.Size[1]; columnIndex++)
            {
                sortedArray = sortedArray.Concat(ilArray[ILMath.full, columnIndex][rowsIndices], 1);
            }

            return sortedArray;
        }
    }

    public class FarmControl
    {
        public static double[][] Simulation(WakeFarmControlConfig config)
        {
            var parm = new WindTurbineParameters();

            ILMatFile env;
            ILMatFile wt;

            ILArray<int> idx;

            double Mg_max_rate;
            double Ki, Kp, Umax, Umin;
            double VS_CtInSp, VS_RtGnSp, VS_Rgn2K;
            double omega0, beta0, power0;

            ILArray<double> initMatrix;
            ILArray<double> sumPower;
            ILArray<double> sumRef;
            ILArray<double> sumAvai;
            ILArray<double> P_ref_new;
            ILArray<double> P_demand;
            ILArray<double> v_nac;
            ILArray<double> P_ref;
            ILArray<double> Pa;
            ILArray<double> Power;
            ILArray<double> beta;
            ILArray<double> Omega;
            ILArray<double> initVector;

            ILArray<double> Mg;
            ILArray<double> wField;

            double dZ;
            ILArray<double> du;
            ILArray<double> x;
            ILArray<double> u;

            ILArray<double> dx;

            ILArray<double> time;
            ILArray<double> dataOut;
            ILArray<double> plotsData;

            //% Initialization
            //bool enablePowerDistribution = true; // Enable wind farm control and not only constant power
            //bool enableVaryingDemand = true; // Varying Reference

            var wind = new ILMatFile(config.Wind_MatFile);

            // Wind farm and Turbine Properties properties
            //parm.wf = LoadILArrayFromFile(config.InitialData_MatFile); // Loads the Wind Farm Layout.
            parm.wf = ((ILArray<double>)(config.Turbines)).T;
            parm.N = parm.wf.length(); // number of turbines in farm
            parm.rotA = -48.80; // Angle of Attack
            parm.kWake = 0.06;

            //% Turbine properties - Loaded from the NREL5MW.mat file
            env = wt = new ILMatFile(config.NREL5MW_MatFile); // Load parameters from the NREL 5MW Reference turbine struct.
            parm.rho = (double)(env.GetArray<double>("env_rho")); // air density
            parm.radius = ((double)(wt.GetArray<double>("wt_rotor_radius"))) * ILMath.ones(1, parm.N); // rotor radius (NREL5MW)
            parm.rated = ((double)(wt.GetArray<double>("wt_ctrl_p_rated"))) * ILMath.ones(1, parm.N); //rated power (NREL5MW)
            parm.ratedSpeed = (double)(wt.GetArray<double>("wt_rotor_ratedspeed")); //rated rotor speed

            idx = ILMath.empty<int>();
            ILMath.max(wt.GetArray<double>("wt_cp_table")[ILMath.full], idx); // Find index for max Cp
            int stepsCount = (int)((config.SimParm.tEnd - config.SimParm.tStart) / config.SimParm.timeStep);
            parm.Ct = 0.0 * wt.GetArray<double>("wt_ct_table").GetValue(idx.ToArray()) * ILMath.ones(parm.N, stepsCount); // Define initial Ct as the optimal Ct. 
            parm.Cp = wt.GetArray<double>("wt_cp_table").GetValue(idx.ToArray()) * ILMath.ones(parm.N, stepsCount); // Define initial Cp as the optimal Cp. 

            Mg_max_rate = (double)wt.GetArray<double>("wt_ctrl_torq_ratelim"); // Rate-limit on Torque Change.

            //Pitch control
            Ki = (double)wt.GetArray<double>("wt_ctrl_pitch_Igain"); // 0.008068634*360/2/pi; % integral gain (NREL5MW).
            Kp = (double)wt.GetArray<double>("wt_ctrl_pitch_Pgain"); // 0.01882681*360/2/pi; % proportional gain (NREL5MW).
            Umax = (double)wt.GetArray<double>("wt_ctrl_pitch_ulim"); // Upper limit of the pitch controller
            Umin = (double)wt.GetArray<double>("wt_ctrl_pitch_llim"); // Lower limit of the pitch controller.

            // NREL Regional Control - extracted from the NREL report. 
            VS_CtInSp = 70.162240;
            VS_RtGnSp = 121.680500;
            VS_Rgn2K = 2.332287;

            //% Set initial conditions
            omega0 = (double)(wt.GetArray<double>("wt_rotor_ratedspeed")); // Desired Rotation speed
            beta0 = 0; // wt.ctrl.pitch.llim; % Initial pitch at zero.
            power0 = 0; // Power Production

            //% Memory Allocation and Memory Initialization
            initMatrix = ILMath.zeros(parm.N, stepsCount);
            sumPower = initMatrix[0, ILMath.full]; // Initialize produced power vector
            sumRef = initMatrix[0, ILMath.full]; // Initialize reference power vector
            sumAvai = initMatrix[0, ILMath.full]; // Initialize available power vector
            P_ref_new = initMatrix[0, ILMath.full]; // Initialize new reference vector
            P_demand = initMatrix[0, ILMath.full]; // Initialize power demand vector
            v_nac = initMatrix.C; // Initialize hub velocity matrix.
            P_ref = initMatrix.C; // Initialize matrix to save the power production history for each turbine.
            Pa = initMatrix.C; // Initialize available power matrix.
            Power = initMatrix.C; // Initialize individual WT power production matrix.
            beta = initMatrix.C; // Initialize pitch matrix.
            Omega = initMatrix.C; // Initialize revolutional velocity matrix.

            initVector = ILMath.ones(parm.N, 1);
            Mg = 0 * initVector;
            wField = ILMath.zeros(config.SimParm.grid / config.SimParm.gridRes, config.SimParm.grid / config.SimParm.gridRes); // Wind field matrix

            //% Controller Initizliation
            dZ = 0; // Integrator Initialization.
            du = ILMath.zeros(parm.N, 1); // Integration variable. 
            x = (omega0 * initVector).Concat(0 * initVector, 1); // x0
            u = (beta0 * initVector).Concat(power0 * initVector, 1); // u0
            P_demand[0] = config.InitialPowerDemand; // Power Demand.

            var turbineModel = new TurbineDrivetrainModel();

            //% Simulate wind farm operation
            for (var i = 2; i <= stepsCount; i++) // At each sample time (DT) from Tstart to Tend
            {
                //clc;
                //fprintf('Iteration Counter: %i out of %i \n', i, config.SimParm.tEnd * (1 / config.SimParm.timeStep));

                //%%%%%%%%%%%%%%% WIND FIELD FIFO MATRIX %%%%%%%%%%%%%%%%%%%
                wField[ILMath.full, ILMath.r(1, ILMath.end)] = wField[ILMath.full, ILMath.r(0, ILMath.end - 1)];
                wField[ILMath.full, 0] = wind.GetArray<double>("wind").GetValue(i - 1, 1) * ILMath.ones(config.SimParm.grid / config.SimParm.gridRes, 1) + ILMath.randn(config.SimParm.grid / config.SimParm.gridRes, 1) * 0.5;
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%  

                // Calculate the wake using the last Ct values
                ILArray<double> v_nacRow;
                wakeCalculationsRLC.Calculate(parm.Ct[ILMath.full, i - 2], wField.T, x[ILMath.full, 1], parm, config.SimParm, out v_nacRow);
                v_nac[ILMath.full, i - 1] = v_nacRow;
                x[ILMath.full, 1] = v_nac[ILMath.full, i - 1];


                if (config.enableVaryingDemand) // A random walk to simulate fluctuations in the power demand.
                {
                    P_demand[i - 1] = P_demand.GetValue(i - 2) + ILMath.randn() * 50000;
                }
                else
                {
                    P_demand[i - 1] = P_demand.GetValue(i - 2);
                }

                // Farm control
                // Calculate the power distribution references for each turbine
                if (config.enablePowerDistribution)
                {
                    ILArray<double> Pa_i_out;
                    PowerDistributionControl.DistributePower(x[ILMath.full, 1], P_demand.GetValue(i - 1), parm, out P_ref_new, out Pa_i_out);
                    Pa[ILMath.full, i - 1] = Pa_i_out;
                }

                //Hold  the demand for some seconds
                if (ILMath.mod(i, ILMath.round(config.SimParm.ctrlUpdate / config.SimParm.timeStep)) == config.SimParm.powerUpdate)
                {
                    P_ref[ILMath.full, i - 1] = P_ref_new;
                }
                else
                {
                    if (config.powerRefInterpolation)
                    {
                        var alpha = 0.01;
                        P_ref[ILMath.full, i - 1] = (1 - alpha) * P_ref[ILMath.full, i - 2] + (alpha) * P_ref_new;
                    }
                    else
                    {
                        P_ref[ILMath.full, i - 1] = P_ref_new;
                    }
                }
                //Torque controller
                for (var j = 1; j <= parm.N; j++)
                {
                    if ((x.GetValue(j - 1, 0) * 97 >= VS_RtGnSp) || (u.GetValue(j - 1, 0) >= 1))      //! We are in region 3 - power is constant
                    {
                        u.SetValue(P_ref.GetValue(j - 1, i - 1) / x.GetValue(j - 1, 0), j - 1, 1);
                    }
                    else if (x.GetValue(j - 1, 0) * 97 <= VS_CtInSp)                     //! We are in region 1 - torque is zero
                    {
                        u.SetValue(0.0, j - 1, 1);
                    }
                    else                                                //! We are in region 2 - optimal torque is proportional to the square of the generator speed
                    {
                        u.SetValue(97 * VS_Rgn2K * x.GetValue(j - 1, 0) * x.GetValue(j - 1, 0) * Math.Pow(97, 2), j - 1, 1);
                    }
                }

                dx = (omega0 - x[ILMath.full, 0]) - (omega0 - Omega[ILMath.full, i - 2]);
                du = Kp * dx + Ki * config.SimParm.timeStep * (omega0 - x[ILMath.full, 0]);
                du = ILMath.min(ILMath.max(du, -((wt.GetArray<double>("wt_ctrl_pitch_ratelim")))), (wt.GetArray<double>("wt_ctrl_pitch_ratelim")));
                u[ILMath.full, 0] = ILMath.min(ILMath.max(u[ILMath.full, 0] + du * config.SimParm.timeStep, Umin), Umax);


                Mg[ILMath.full, i - 1] = u[ILMath.full, 1]; // Torque Input
                beta[ILMath.full, i - 1] = u[ILMath.full, 0]; // Pitch Input

                // Turbine dynamics - can be simplified:
                if (config.enableTurbineDynamics)
                {
                    for (var j = 1; j <= parm.N; j++)
                    {
                        double xItemValue;
                        double parmCtItemValue;
                        double parmCpItemValue;
                        turbineModel.Model(x[j - 1, ILMath.full], u[j - 1, ILMath.full], wt, env, config.SimParm.timeStep, out xItemValue, out parmCtItemValue, out parmCpItemValue);
                        //[x(j,1), parm.Ct(j,i), parm.Cp(j,i)]
                        x.SetValue(xItemValue, j - 1, 0);
                        parm.Ct.SetValue(parmCtItemValue, j - 1, i - 1);
                        parm.Cp.SetValue(parmCpItemValue, j - 1, i - 1);
                    }
                }
                else
                {
                    x[ILMath.full, 0] = parm.ratedSpeed; // Rotational speed
                }

                Omega[ILMath.full, i - 1] = x[ILMath.full, 0];
                Power[ILMath.full, i - 1] = Omega[ILMath.full, i - 1] * Mg[ILMath.full, i - 1];

                // Power Summations
                sumPower.SetValue((double)(ILMath.sum(Power[ILMath.full, i - 1])) * 1E-6, i - 1);
                sumRef.SetValue((double)(ILMath.sum(P_ref[ILMath.full, i - 1])) * 1E-6, i - 1);
                sumAvai.SetValue((double)(ILMath.sum(Pa[ILMath.full, i - 1])) * 1E-6, i - 1);

                // NOWCASTING FUNKTION HER
                // powerPrediction(i) = powerPrediction(i,sumPower(i:-1:i-10)) % or something
                // similar.
            }

            //%
            //time    = (simParm.tStart:simParm.timeStep:simParm.tEnd-simParm.timeStep)';
            time = (ILMath.counter(config.SimParm.tStart, config.SimParm.timeStep, stepsCount));  // (config.SimParm.tEnd - config.SimParm.tStart) / config.SimParm.timeStep


            if (config.saveData)
            {
                dataOut = time.C;
                dataOut = dataOut.Concat(sumPower.T, 1);
                dataOut = dataOut.Concat(sumRef.T, 1);
                dataOut = dataOut.Concat(sumAvai.T, 1);
                //save dataOut;
            }
            //% Plotting
            //Below a number of different plots are made. Most of them for test purposes
            plotsData = time.C;
            //f1 = figure(1); //clf;
            //plot(time,P_ref*(10^(-6))); grid on; // Power Reference
            //xlabel('Time [s]'); ylabel('Power Reference [MW]');
            //title('Individual Power Reference');
            plotsData = plotsData.Concat(P_ref.T * 1E-6, 1);
            // 
            //f2 = figure(2); clf;
            //plot(time,v_nac); grid on; // Wind velocity at each turbine
            //xlabel('Time [s]'); ylabel('Wind Speed [m/s]');
            //title('Wind Speed @ individual turbine');
            plotsData = plotsData.Concat(v_nac.T, 1);

            //f4 = figure(4); clf;
            //plot(time,beta'); grid on;
            //xlabel('Time [s]'); ylabel('Pitch Angle [deg]');
            //title('Evolution of Pitch angle over time');
            plotsData = plotsData.Concat(beta.T, 1);

            //f5 = figure(5); clf;
            //plot(time,Omega'); grid on;
            //xlabel('Time [s]'); ylabel('Revolutional Velocity [rpm]');
            //title('Evolution of Revolutional Velocity over time');
            plotsData = plotsData.Concat(Omega.T, 1);

            //f3 = figure(3); clf;
            //plot(time,sumRef,time,sumAvai,time,sumPower); grid on; // Power References
            //xlabel('Time [s]'); ylabel('Power [MW]');
            //legend('Reference','Available','Produced');
            //title('Power Plot');
            plotsData = plotsData.Concat(sumRef.T, 1);
            plotsData = plotsData.Concat(sumAvai.T, 1);
            plotsData = plotsData.Concat(sumPower.T, 1);

            return plotsData.ToDoubleArray();
        }
    }
}
