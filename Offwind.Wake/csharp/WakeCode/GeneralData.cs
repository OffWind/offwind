namespace WakeCode
{
    public class GeneralData
    {
        public System.Int32 TurbinesAmount;
        public System.Int32 GridPointsX;
        public System.Int32 GridPointsY;
        public double RotationAngle;

        public double TurbineThrust;
        public double TurbineDiameter;
        public double WakeDecay;
        public double TurbineHeight;
        public double VelocityAtHub;
        public double AirDensity;
        public double PowerDistance;

        public double[] x_turb;     // location of the turbine
        public double[] y_turb;     // location of the turbine
    }
}