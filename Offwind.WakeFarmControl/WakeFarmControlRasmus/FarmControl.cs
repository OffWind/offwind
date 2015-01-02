using System;
using ILNumerics;
using MatlabInterpreter;

namespace WakeFarmControlR
{
    internal partial class TranslatedCode : MatlabCode
    {
        protected static ILArray<double> ILArrayFromArray(double[,] array)
        {
            return ((ILArray<double>)array).T;
        }

        private static ILArray<double> load(double[,] array)
        {
            return ILArrayFromArray(array);
        }

        private static void load(string windMatFilePath, out ILArray<double> wind)
        {
            using (var WindMatFile = new ILMatFile(windMatFilePath))
            {
                wind = WindMatFile.GetArray<double>("wind");
            }
        }

        private static void load(string nrel5MWMatFilePath, out EnvMatFileDataStructure env, out WtMatFileDataStructure wt)
        {
            using (var NREL5MWMatFile = new ILMatFile(nrel5MWMatFilePath))
            {
                env = new EnvMatFileDataStructure(NREL5MWMatFile);
                wt = new WtMatFileDataStructure(NREL5MWMatFile);
            }
        }

        public static double[][] FarmControl(WakeFarmControlConfig config)
        {
            #region "Used variables declaration"
            bool saveData;
            bool enablePowerDistribution;
            bool enableTurbineDynamics;
            bool powerRefInterpolation;
            bool enableVaryingDemand;
            SimParm simParm;

            ILArray<double> wind;
            WindTurbineParameters parm;
            EnvMatFileDataStructure env;
            WtMatFileDataStructure wt;
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
            double alpha;
            int j;
            ILArray<double> dx;
            ILArray<double> time;
            ILArray<double> dataOut;
            #endregion

            //% Initialization
            //General settings to be changed
            saveData                = config.saveData; // Save all the simulated data to a .mat file?
            enablePowerDistribution = config.enablePowerDistribution; // Enable wind farm control and not only constant power
            enableTurbineDynamics   = config.enableTurbineDynamics; // Enable dynamical turbine model. Disabling this will increase the speed significantly, but also lower the fidelity of the results (setting to false does not work properly yet)
            powerRefInterpolation   = config.powerRefInterpolation; // Power Reference table interpolation.
            enableVaryingDemand     = config.enableVaryingDemand; // Varying Reference

            // Simulation Properties:
            simParm = new SimParm();
            simParm.tStart      = config.SimParm.tStart; // time start
            simParm.timeStep    = config.SimParm.timeStep; // time step, 8Hz - the NREL model is 80Hz (for reasons unknown)
            simParm.tEnd        = config.SimParm.tEnd; // time end
            int _simParm_tEnd_simParm_tStart__simParm_timeStep = (int)((simParm.tEnd - simParm.tStart) / simParm.timeStep);
            simParm.gridRes     = config.SimParm.gridRes; // Grid Resolution
            simParm.grid        = config.SimParm.grid; // Grid Size
            simParm.ctrlUpdate  = config.SimParm.ctrlUpdate;  // Update inverval for farm controller
            simParm.powerUpdate = config.SimParm.powerUpdate; // How often the control algorithm should update!
            load(config.Wind_MatFile, out wind); // Load Wind Data

            // Wind farm and Turbine Properties properties
            parm = new WindTurbineParameters();
            parm.wf = load(config.Turbines); // Loads the Wind Farm Layout.
            parm.N = length(parm.wf); // number of turbines in farm
            parm.rotA = -48.80; // Angle of Attack
            parm.kWake = 0.06;

            //% Turbine properties - Loaded from the NREL5MW.mat file
            load(config.NREL5MW_MatFile, out env, out wt); // Load parameters from the NREL 5MW Reference turbine struct.
            parm.rho = env.rho; // air density
            parm.radius = wt.rotor.radius * ones(1, parm.N); // rotor radius (NREL5MW)
            parm.rated = wt.ctrl.p_rated * ones(1, parm.N); //rated power (NREL5MW)
            parm.ratedSpeed = wt.rotor.ratedspeed; //rated rotor speed
            max(out idx, wt.cp.table[_(':')]); // Find index for max Cp
            parm.Ct = 0.0 * wt.ct.table[_(idx)] * ones(parm.N, (_simParm_tEnd_simParm_tStart__simParm_timeStep)); // Define initial Ct as the optimal Ct. 
            parm.Cp = wt.cp.table[_(idx)] * ones(parm.N, (_simParm_tEnd_simParm_tStart__simParm_timeStep)); // Define initial Cp as the optimal Cp. 
            Mg_max_rate = wt.ctrl.torq.ratelim; // Rate-limit on Torque Change.

            //Pitch control
            Ki = wt.ctrl.pitch.Igain; // 0.008068634*360/2/pi; % integral gain (NREL5MW).
            Kp = wt.ctrl.pitch.Pgain; // 0.01882681*360/2/pi; % proportional gain (NREL5MW).
            Umax = wt.ctrl.pitch.ulim; // Upper limit of the pitch controller
            Umin = wt.ctrl.pitch.llim; // Lower limit of the pitch controller.

            // NREL Regional Control - extracted from the NREL report. 
            VS_CtInSp = 70.162240;
            VS_RtGnSp = 121.680500;
            VS_Rgn2K = 2.332287;

            //% Set initial conditions
            omega0 = wt.rotor.ratedspeed; // Desired Rotation speed
            beta0 = 0; // wt.ctrl.pitch.llim; % Initial pitch at zero.
            power0 = 0; // Power Production

            //% Memory Allocation and Memory Initialization
            initMatrix = zeros(parm.N, _simParm_tEnd_simParm_tStart__simParm_timeStep);
            sumPower = initMatrix[_(1), _(':')]; // Initialize produced power vector
            sumRef = initMatrix[_(1), _(':')]; // Initialize reference power vector
            sumAvai = initMatrix[_(1), _(':')]; // Initialize available power vector
            P_ref_new = initMatrix[_(1), _(':')]; // Initialize new reference vector
            P_demand = initMatrix[_(1), _(':')]; // Initialize power demand vector
            v_nac = initMatrix.C; // Initialize hub velocity matrix.
            P_ref = initMatrix.C; // Initialize matrix to save the power production history for each turbine.
            Pa = initMatrix.C; // Initialize available power matrix.
            Power = initMatrix.C; // Initialize individual WT power production matrix.
            beta = initMatrix.C; // Initialize pitch matrix.
            Omega = initMatrix.C; // Initialize revolutional velocity matrix.

            initVector = ones(parm.N, 1);
            Mg = 0 * initVector;
            wField = zeros(simParm.grid / simParm.gridRes, simParm.grid / simParm.gridRes); // Wind field matrix

            //% Controller Initizliation
            dZ = 0; // Integrator Initialization.
            du = zeros(parm.N, 1); // Integration variable. 
            x = __[ omega0 * initVector, 0 * initVector ]; // x0
            u = __[ beta0 * initVector, power0 * initVector ]; // u0
            P_demand._(1, '=', config.InitialPowerDemand); // Power Demand.

            //% Simulate wind farm operation
            for (var i = 2; i <= _simParm_tEnd_simParm_tStart__simParm_timeStep; i++) // At each sample time (DT) from Tstart to Tend
            {
                //clc;
                //fprintf('Iteration Counter: %i out of %i \n', i, config.SimParm.tEnd * (1 / config.SimParm.timeStep));

                //%%%%%%%%%%%%%%% WIND FIELD FIFO MATRIX %%%%%%%%%%%%%%%%%%%
                wField[_(':'), _(2, ':', end)]  = wField[_(':'), _(1, ':', (end - 1))];
                wField[_(':'), _(1)]            = wind._(i, 2) * ones(simParm.grid / simParm.gridRes, 1) + randn(simParm.grid / simParm.gridRes, 1) * 0.5;
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%  

                // Calculate the wake using the last Ct values
                ILArray<double> v_nac___i_;
                wakeCalculationsRLC(out v_nac___i_, parm.Ct[_(':'), _(i - 1)], transpose(wField), x[_(':'), _(2)], parm, simParm);
                v_nac[_(':'), _(i)] = v_nac___i_;
                x[_(':'), _(2)]     = v_nac[_(':'), _(i)];


                if (enableVaryingDemand) // A random walk to simulate fluctuations in the power demand.
                {
                    P_demand._(i, '=', P_demand._(i - 1) + randn() * 50000);
                }
                else
                {
                    P_demand._(i, '=', P_demand._(i - 1));
                }

                // Farm control
                // Calculate the power distribution references for each turbine
                if (enablePowerDistribution)
                {
                    ILArray<double> Pa___i_;
                    powerDistributionControl(out P_ref_new, out Pa___i_, x[_(':'), _(2)], P_demand._(i), parm);
                    Pa[_(':'), _(i)] = Pa___i_;
                }

                //Hold  the demand for some seconds
                if (mod(i, round(simParm.ctrlUpdate / simParm.timeStep)) == simParm.powerUpdate)
                {
                    P_ref[_(':'), _(i)] = P_ref_new;
                }
                else
                {
                    if (powerRefInterpolation)
                    {
                        alpha = 0.01;
                        P_ref[_(':'), _(i)] = (1 - alpha) * P_ref[_(':'), _(i - 1)] + (alpha) * P_ref_new;
                    }
                    else
                    {
                        P_ref[_(':'), _(i)] = P_ref_new;
                    }
                }
                //Torque controller
                for (j = 1; j <= parm.N; j++)
                {
                    if ((x._(j, 1) * 97 >= VS_RtGnSp) || (u._(j, 1) >= 1))      //! We are in region 3 - power is constant
                    {
                        u._(j, 2,   '=', P_ref._(j, i) / x._(j, 1));
                    }
                    else if (x._(j, 1) * 97 <= VS_CtInSp)                     //! We are in region 1 - torque is zero
                    {
                        u._(j, 2,   '=', 0.0);
                    }
                    else                                                //! We are in region 2 - optimal torque is proportional to the square of the generator speed
                    {
                        u._(j, 2,   '=', 97 * VS_Rgn2K * x._(j, 1) * x._(j, 1) * _p(97, 2));
                    }
                }

                dx              = (omega0 - x[_(':'), _(1)]) - (omega0 - Omega[_(':'), _(i - 1)]);
                du              = Kp * dx + Ki * simParm.timeStep * (omega0 - x[_(':'), _(1)]);
                du              = min(max(du, -wt.ctrl.pitch.ratelim), wt.ctrl.pitch.ratelim);
                u[_(':'), _(1)] = min(max(u[_(':'), _(1)] + du * simParm.timeStep, Umin), Umax);


                Mg[_(':'), _(i)]    = u[_(':'), _(2)]; // Torque Input
                beta[_(':'), _(i)]  = u[_(':'), _(1)]; // Pitch Input

                // Turbine dynamics - can be simplified:
                if (enableTurbineDynamics)
                {
                    for (j = 1; j <= parm.N; j++)
                    {
                        double x_j_1_; double parm_Ct_j_i_; double parm_Cp_j_i_;
                        turbineDrivetrainModel(out x_j_1_, out parm_Ct_j_i_, out parm_Cp_j_i_, x[_(j), _(':')], u[_(j), _(':')], wt, env, simParm.timeStep);
                        x._(j, 1, '=', x_j_1_); parm.Ct._(j, i, '=', parm_Ct_j_i_); parm.Cp._(j, i, '=', parm_Cp_j_i_);
                    }
                }
                else
                {
                    x[_(':'), _(1)] = parm.ratedSpeed; // Rotational speed
                }

                Omega[_(':'), _(i)] = x[_(':'), _(1)];
                Power[_(':'), _(i)] = Omega[_(':'), _(i)] * Mg[_(':'), _(i)];

                // Power Summations
                sumPower._(i,   '=', sum_(Power[_(':'), _(i)]) * _p(10, -6));
                sumRef._(i,     '=', sum_(P_ref[_(':'), _(i)]) * _p(10, -6));
                sumAvai._(i,    '=', sum_(Pa[_(':'), _(i)]) * _p(10, -6));

                // NOWCASTING FUNKTION HER
                // powerPrediction(i) = powerPrediction(i,sumPower(i:-1:i-10)) % or something
                // similar.
            }

            //%
            time = (_c(simParm.tStart, simParm.timeStep, simParm.tEnd - simParm.timeStep)).T;

            if (saveData)
            {
                //throw new ApplicationException(string.Format("{0}\t{1}\t{2}\t{3}", time.Dimensions, sumPower.T.Dimensions, sumRef.T.Dimensions, sumAvai.T.Dimensions));
                dataOut = __[ time, sumPower.T, sumRef.T, sumAvai.T ];
                //save dataOut;
            }
            //% Plotting
            //Below a number of different plots are made. Most of them for test purposes
            ILArray<double> plotsData;
            plotsData = time.C;
            plotsData = __[plotsData, P_ref.T * (_p(10, -6))];              // f1 = figure(1); xlabel('Time [s]'); ylabel('Power Reference [MW]'); title('Individual Power Reference');
            plotsData = __[ plotsData, v_nac.T ];                           // f2 = figure(2); xlabel('Time [s]'); ylabel('Wind Speed [m/s]'); title('Wind Speed @ individual turbine');
            plotsData = __[ plotsData, beta.T ];                            // f4 = figure(4); xlabel('Time [s]'); ylabel('Pitch Angle [deg]'); title('Evolution of Pitch angle over time');
            plotsData = __[ plotsData, Omega.T ];                           // f5 = figure(5); xlabel('Time [s]'); ylabel('Revolutional Velocity [rpm]'); title('Evolution of Revolutional Velocity over time');
            plotsData = __[ plotsData, sumRef.T, sumAvai.T, sumPower.T ];   // f3 = figure(3); xlabel('Time [s]'); ylabel('Power [MW]'); title('Power Plot'); legend('Reference','Available','Produced');

            return plotsData.ToDoubleArray();
        }
    }
}
