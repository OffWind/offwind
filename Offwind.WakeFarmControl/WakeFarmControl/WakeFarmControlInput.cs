using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakeFarmControl
{
    public sealed class WakeFarmControlConfig
    {
        public double Tstart;                // time start
        public double Tend;                  // time end
        public double DT;                    // time step
        public bool EnablePowerDistribution; // enable wind farm control and not only constant power
        public bool EnableTurbineDynamics;   //Enable dynamical turbine model.
                                             //Disabling this will increase the speed significantly, but also lower the fidelity of the results (setting to false does not work properly yet)
        public int PRefSampleTime;           // Update inverval for farm controller
        public int NTurbines;                // Turbines count in farm
        public double Pdemand;               // some number

        public string NREL5MW_MatFile;
        public string Wind_MatFile;

        public bool PowerRefInterpolation;

        public int TimeLine()
        {
            return (int) ((Tend - Tstart)/DT);
        }
    }
}
