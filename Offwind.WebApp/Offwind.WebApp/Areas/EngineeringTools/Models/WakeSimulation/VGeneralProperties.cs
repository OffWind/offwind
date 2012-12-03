namespace MvcApplication1.Areas.EngineeringTools.Models.WakeSimulation
{
    public class VGeneralProperties
    {
        public string SolverState { get; set; }

        public string SolverOutputDir { get; set; }

        public int GridPointsX { get; set; }
        
        public int GridPointsY { get; set; }

        public decimal TurbineDiameter { get; set; }

        public decimal TurbineHeight { get; set; }

        public decimal TurbineThrust { get; set; }

        public decimal WakeDecay { get; set; }

        public decimal VelocityAtHub { get; set; }

        public decimal AirDensity { get; set; }

        public decimal PowerDistance { get; set; }
       
        public decimal RotationAngle { get; set; }
    }
}