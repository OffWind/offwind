using System;
using System.Linq;
using ILNumerics;

namespace WakeFarmControl
{
    public sealed class FarmControl
    {
        //% The main file for running the wind farm controll and wake simulation.
        // It is not completely done yet. Further updates will come
        // Currently there are only 4 turbines, for test purposes. But is should be
        // easily updated to a larger number of turbines.
        // Similarly there is a lot of room for speed optimizations, even though it
        // now runs slowly with only 4 turbines
        // 19/07-13 MS

        public static double[][] Simulation(WakeFarmControlConfig config)
        {
            var parm = new WindTurbineParameters();

            ILMatFile env;
            ILMatFile wt;

            ILArray<int> idx;
            ILArray<double> ee;

            double Ki;
            double Kp;
            int PC_MaxPit;
            int PC_MinPit;
            double VS_CtInSp;
            double VS_RtGnSp;
            double VS_Rgn2K;
            double omega0;
            double beta0;
            double power0;
            ILArray<double> x;
            ILArray<double> u0;
            ILArray<double> u;
            ILArray<double> Mg_old;
            ILArray<double> P_ref;
            ILArray<double> Pa;
            ILArray<double> Power;
            ILArray<double> Ct;
            ILArray<double> P_ref_new;
            ILArray<double> v_nac;
            double alpha;
            double Mg_max_rate;
            ILArray<double> e;
            ILArray<double> Mg;
            ILArray<double> beta;
            ILArray<double> Cp;
            ILArray<double> Omega;
            ILArray<double> out_;

            if (config.NTurbines == 0) return null;

            // Wind farm properties
            //turbine properties
            env = wt = new ILMatFile(config.NREL5MW_MatFile); //Load parameters from the NREL 5MW turbine
            parm.N = config.NTurbines; // number of turbines in farm
            parm.rho = (double)env.GetArray<double>("env_rho"); //air density
            parm.radius = ((double)(wt.GetArray<double>("wt_rotor_radius"))) * ILMath.ones(1, config.NTurbines); // rotor radius (NREL5MW)
            parm.rated = 5e6 * ILMath.ones(1, config.NTurbines); //rated power (NREL5MW)
            parm.ratedSpeed = (double)wt.GetArray<double>("wt_rotor_ratedspeed"); //rated rotor speed

            idx = ILMath.empty<int>();
            ILMath.max(wt.GetArray<double>("wt_cp_table")[ILMath.full], idx); //Find index for max Cp;
            parm.Cp = ILMath.ones(1, config.NTurbines) * wt.GetArray<double>("wt_cp_table").GetValue(idx.ToArray()); //Set power coefficent to maximum value in the cp table
            parm.Ct = ILMath.ones(1, config.NTurbines) * wt.GetArray<double>("wt_ct_table").GetValue(idx.ToArray()); //Set power coefficent to maximum value in the ct table

            // NOTE: controller parameters should be imported from the wt....struct in
            //Pitch control

            ee = 0; //blade pitch integrator
            Ki = 0.008068634 * 360 / 2 / ILMath.pi; // integral gain (NREL5MW)
            Kp = 0.01882681 * 360 / 2 / ILMath.pi; // proportional gain (NREL5MW)

            PC_MaxPit = 90;
            PC_MinPit = 0;

            //region control NREL
            VS_CtInSp = 70.16224;
            VS_RtGnSp = 121.6805;
            VS_Rgn2K = 2.332287;


            // load initial wind data
            var wind = new ILMatFile(config.Wind_MatFile);

            //% Set initial conditions
            omega0 = 1.267; //Rotation speed
            beta0 = 0;      //Pitch

            var timeLine = (int)config.TimeLine();

            power0 = parm.rated.GetValue(0); //Power production
            x = (omega0 * ILMath.ones(parm.N, 1)).Concat((wind.GetArray<double>("wind").GetValue(0, 1) * ILMath.ones(parm.N, 1)), 1);
            u0 = (beta0 * ILMath.ones(parm.N, 1)).Concat((power0 * ILMath.ones(parm.N, 1)), 1);
            u = u0.C;
            Mg_old = u[ILMath.full, 1];
            P_ref = ILMath.zeros(parm.N, (int)config.TimeLine()); //Initialize matrix to save the power production history for each turbine
            Pa = P_ref.C; //Initialize available power matrix
            Power = P_ref.C;
            Ct = parm.Ct.C; //Initialize Ct - is this correct?
            Ct[timeLine - 1, ILMath.full] = Ct[0, ILMath.full];
            P_ref_new = power0 * ILMath.ones(config.NTurbines, 1);

            v_nac = ILMath.zeros(Ct.Size[1], timeLine);
            Mg = ILMath.zeros(u.Size[0], timeLine);
            beta = ILMath.zeros(u.Size[0], timeLine);
            Omega = ILMath.zeros(Ct.Size[1], timeLine);
            Cp = ILMath.zeros(timeLine, parm.Cp.Size[1]);

            var turbineModel = new TurbineDrivetrainModel();

            //% Simulate wind farm operation
            //var timeLine = (int) config.TimeLine();
            for (var i = 2; i <= timeLine; i++) //At each sample time(DT) from Tstart to Tend
            {
                //Calculate the wake using the current Ct values
                {
                    ILArray<double> out_v_nac;
                    WakeCalculation.Calculate((Ct[i - 1 - 1, ILMath.full]), i, wind, out out_v_nac);
                    v_nac[ILMath.full, i - 1] = out_v_nac;
                }
                x[ILMath.full, 1] = v_nac[ILMath.full, i - 1];


                //Farm control
                //Calculate the power distribution references for each turbine
                if (config.EnablePowerDistribution)
                {
                    ILArray<double> out_Pa;
                    PowerDistributionControl.DistributePower(v_nac[ILMath.full, i - 1], config.Pdemand, Power[ILMath.full, i - 1 - 1], parm, out P_ref_new, out out_Pa);
                    Pa[ILMath.full, i - 1] = out_Pa;
                }

                //Hold  the demand for some seconds
                if (ILMath.mod(i, ILMath.round(config.PRefSampleTime / config.DT)) == 2) //???
                {
                    P_ref[ILMath.full, i - 1] = P_ref_new;
                }
                else
                {
                    if (config.PowerRefInterpolation)
                    {
                        alpha = 0.01;
                        P_ref[ILMath.full, i - 1] = (1 - alpha) * P_ref[ILMath.full, i - 1 - 1] + (alpha) * P_ref_new;
                    }
                    else
                    {
                        P_ref[ILMath.full, i - 1] = P_ref_new;
                    }
                }


                //Calculate control for each individual turbine - should be moved to the
                //turbine (drivetrain) model.

                //Torque controller
                for (var j = 1; j <= parm.N; j++)
                {
                    if ((x.GetValue(j - 1, 0) * 97 >= VS_RtGnSp) || (u.GetValue(j - 1, 0) >= 1))   // We are in region 3 - power is constant
                    {
                        u.SetValue(P_ref.GetValue(j - 1, i - 1) / x.GetValue(j - 1, 0), j - 1, 1);
                    }
                    else if (x.GetValue(j - 1, 0) * 97 <= VS_CtInSp)                            //! We are in region 1 - torque is zero
                    {
                        u.SetValue(0.0, j - 1, 1);
                    }
                    else                                                         //! We are in region 2 - optimal torque is proportional to the square of the generator speed
                    {
                        u.SetValue(97 * VS_Rgn2K * x.GetValue(j - 1, 0) * x.GetValue(j - 1, 0) * Math.Pow(97, 2), j - 1, 1);
                    }
                }

                //Rate limit torque change
                //  u(:,2) - Mg_old;
                Mg_max_rate = 1e6 * config.DT;
                u[ILMath.full, 1] = ILMath.sign(u[ILMath.full, 1] - Mg_old) * ILMath.min(ILMath.abs(u[ILMath.full, 1] - Mg_old), Mg_max_rate) + Mg_old;

                //Pitch controller
                e = 97 * (omega0 * ILMath.ones(parm.N, 1) - x[ILMath.full, 0]);
                ee = ee - config.DT * e;
                ee = ILMath.min(ILMath.max(ee, PC_MinPit / Ki), PC_MaxPit / Ki);

                u[ILMath.full, 0] = -Kp * config.DT * e + Ki * ee;
                for (var j = 1; j <= parm.N; j++)
                {
                    u.SetValue(Math.Min(Math.Max(u.GetValue(j - 1, 0), PC_MinPit), PC_MaxPit), j - 1, 0);
                }

                if (!config.EnableTurbineDynamics)
                {
                    u = u0;
                }

                Mg[ILMath.full, i - 1] = u[ILMath.full, 1];
                Mg_old = Mg[ILMath.full, i - 1];
                beta[ILMath.full, i - 1] = u[ILMath.full, 0]; //Set pitch


                //Turbine dynamics - can be simplified
                if (config.EnableTurbineDynamics)
                {

                    for (var j = 1; j <= parm.N; j++)
                    {
                        double out_x;
                        double out_Ct;
                        double out_Cp;
                        turbineModel.Model(x[j - 1, ILMath.full], u[j - 1, ILMath.full], wt, env, config.DT, out out_x, out out_Ct, out out_Cp);
                        x.SetValue(out_x, j - 1, 0);
                        Ct.SetValue(out_Ct, i - 1, j - 1);
                        Cp.SetValue(out_Cp, i - 1, j - 1);
                    }
                }
                else
                {
                    Ct[i - 1, ILMath.full] = parm.Ct;
                    Cp[i - 1, ILMath.full] = parm.Cp;
                    x[ILMath.full, 0] = parm.ratedSpeed;//Rotational speed
                }

                Omega[ILMath.full, i - 1] = x[ILMath.full, 0];
                Power[ILMath.full, i - 1] = Omega[ILMath.full, i - 1] * Mg[ILMath.full, i - 1];
            }

            //% Save output data
            out_ = (config.DT * (ILMath.counter(0, 1, config.TimeLine())));
            out_ = out_.Concat(v_nac.T, 1);
            out_ = out_.Concat(Omega.T, 1);
            out_ = out_.Concat(beta.T, 1);
            out_ = out_.Concat(P_ref.T, 1);
            out_ = out_.Concat(Ct, 1);
            out_ = out_.Concat(Cp, 1);
            out_ = out_.Concat(Pa.T, 1);
            out_ = out_.Concat(Mg.T, 1);
            out_ = out_.Concat(Power.T, 1);

            //Ttotal power demand
            var l = config.NTurbines * 3 + 1;
            var r = l + config.NTurbines - 1;

            out_ = out_.Concat(ILMath.sum(out_[ILMath.full, ILMath.r(l, r)], 1) / 1e6, 1);    // P_ref sum

            l = config.NTurbines * 6 + 1;
            r = l + config.NTurbines - 1;

            out_ = out_.Concat(ILMath.sum(out_[ILMath.full, ILMath.r(l, r)], 1) / 1e6, 1);    // Pa sum. 'Power Demand'
            out_ = out_.Concat(ILMath.sum(Power).T / 1e6, 1);                                   // 'Actual Production'

            //Ttotal power demand
            out_ = out_.Concat(ILMath.sum(P_ref.T, 1), 1);              // 'Demand'
            out_ = out_.Concat(ILMath.sum(Pa.T, 1), 1);                 // 'Available'
            out_ = out_.Concat(ILMath.sum(Mg * Omega).T, 1);            // 'Actual'

            //Total power produced
            out_ = out_.Concat((Mg * Omega).T, 1);

            var out_doubleArray = new double[out_.Size[0]][];
            for (int i = 0; i <= out_doubleArray.GetLength(0) - 1; i++)
            {
                out_doubleArray[i] = new double[out_.Size[1]];
                for (int j = 0; j <= out_doubleArray[i].GetLength(0) - 1; j++)
                {
                    out_doubleArray[i][j] = out_.GetValue(i, j);
                }
            }
            return out_doubleArray;
        }
    }
}
