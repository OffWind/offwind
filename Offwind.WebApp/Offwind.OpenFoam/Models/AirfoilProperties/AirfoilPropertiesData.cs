using System.Collections.Generic;
using Offwind.OpenFoam.Models.AirfoilProperties;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.Sowfa.Constant.AirfoilProperties
{
    public sealed  class AirfoilPropertiesData
    {
        public readonly string[] DefaultAirfoils = {
                                                       AirfoilPropRes.Cylinder1,
                                                       AirfoilPropRes.Cylinder2,
                                                       AirfoilPropRes.DU21_A17,
                                                       AirfoilPropRes.DU25_A17,
                                                       AirfoilPropRes.DU30_A17,
                                                       AirfoilPropRes.DU35_A17,
                                                       AirfoilPropRes.DU40_A17,
                                                       AirfoilPropRes.NACA64_A17
                                                   };

        public readonly string[] DefaultFiles = {
                                                    "Cylinder1",
                                                    "Cylinder2",
                                                    "DU21_A17",
                                                    "DU25_A17",
                                                    "DU30_A17",
                                                    "DU35_A17",
                                                    "DU40_A17",
                                                    "NACA64_A17"
                                                };



        public List<AirfoilPropertiesInstance> collection { set; get; }

        public AirfoilPropertiesData()
        {
            collection = new List<AirfoilPropertiesInstance>();
        }
    }
}
