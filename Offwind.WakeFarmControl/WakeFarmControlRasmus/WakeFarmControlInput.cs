using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakeFarmControlR
{
    public class SimParm
    {
        public double tStart = 0; // time start
        public double timeStep = 0.1; // time step, 8Hz - the NREL model is 80Hz (for reasons unknown)
        public double tEnd = 100; // time end
        public int gridRes = 10; // Grid Resolution
        public int grid = 1000; // Grid Size
        public double ctrlUpdate = 5;  // Update inverval for farm controller
        public double powerUpdate = 1; // How often the control algorithm should update!
    }

    public sealed class WakeFarmControlConfig
    {
        public bool saveData;
        public bool enablePowerDistribution;
        public bool enableTurbineDynamics;
        public bool powerRefInterpolation;
        public bool enableVaryingDemand;

        public SimParm SimParm;
        //public double Tstart;                // time start
        //public double Tend;                  // time end
        //public double DT;                    // time step
        //public bool EnablePowerDistribution; // enable wind farm control and not only constant power
        //public bool EnableTurbineDynamics;   //Enable dynamical turbine model.
        //                                     //Disabling this will increase the speed significantly, but also lower the fidelity of the results (setting to false does not work properly yet)
        //public int PRefSampleTime;           // Update inverval for farm controller
        public double[,] Turbines;             // Turbines coordinates in farm
        //public int NTurbines;                // Turbines count in farm
        //public double Pdemand;               // some number
        public double InitialPowerDemand;       // Initial Power Demand

        public string NREL5MW_MatFile;
        public string Wind_MatFile;
        //public string InitialData_MatFile;

        //public bool PowerRefInterpolation;

        //public double TimeLine()
        //{
        //    return ((Tend - Tstart)/DT);
        //}

        public WakeFarmControlConfig()
        {
            //General settings to be changed
            saveData = true; // Save all the simulated data to a .mat file?
            enablePowerDistribution = true; // Enable wind farm control and not only constant power
            enableTurbineDynamics = true; // Enable dynamical turbine model. Disabling this will increase the speed significantly, but also lower the fidelity of the results (setting to false does not work properly yet)
            powerRefInterpolation = true; // Power Reference table interpolation.
            enableVaryingDemand = true; // Varying Reference

            InitialPowerDemand = 50 * 5e6;

            // Simulation Properties:
            SimParm = new SimParm();
            SimParm.tStart = 0; // time start
            SimParm.timeStep = 0.1; // time step, 8Hz - the NREL model is 80Hz (for reasons unknown)
            SimParm.tEnd = 100; // time end
            SimParm.gridRes = 10; // Grid Resolution
            SimParm.grid = 1000; // Grid Size
            SimParm.ctrlUpdate = 5;  // Update inverval for farm controller
            SimParm.powerUpdate = 1; // How often the control algorithm should update!
        }
    }
}
