namespace Offwind.WebApp.Areas.EngineeringTools.Models.WindWave.Computations
{
    public sealed class WindWaveInput
    {
        public double Ug { get; set; }			//Wind speed measured at the reference height 
        public double Zg { get; set; }			//Reference height
        public double Zhub { get; set; }		//Turbine hub height
        public double Td { get; set; }			//Turbine diameter
        public double Ef { get; set; }			//Turbine efficiency
        public double Cw { get; set; }			//Wave speed
    }
}
