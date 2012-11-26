using Offwind.Common;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;

namespace Offwind.Products.MesoWind
{
    public sealed class MesoWindProjectDescriptor : ProjectDescriptor
    {
        public MesoWindProjectDescriptor()
        {
            Order = 200;
            ProductType = ProductType.EngineeringTools;
            Name = "Mesoscale Wind Tools";
            Code = ProductCode.Engineering_WindWave;
            Description = @"Mesoscale Wind Characterisation gives an easy access to a database which can be used to configure OpenFOAM simulations for OFFWIND projects.";

            //new ProjectItemDescriptor()
            //    .SetEditorGroup(CaseItemType.Project)
            //    .SetDefaultName("World Map")
            //    .SetForm(typeof(CWorldMap))
            //    .SetHandler(typeof(StubFileHandler))
            //    .SetCode("CMesoWind_WorldMap")
            //    .SetNoScroll(true)
            //    .AddTo(DefaultItems);

            new ProjectItemDescriptor()
                .SetDefaultName("Database")
                .SetForm(typeof(CMesoWind))
                .SetHandler(typeof(StubFileHandler))
                .SetCode("CMesoWind_DataImport")
                .SetNoScroll(true)
                .AddTo(DefaultItems);

            new ProjectItemDescriptor()
                .SetDefaultName("Current Data")
                .SetForm(typeof(CImportedData))
                .SetHandler(typeof(StubFileHandler))
                .SetCode("CMesoWind_CurrentData")
                .SetNoScroll(true)
                .AddTo(DefaultItems);

            new ProjectItemDescriptor()
                .SetDefaultName("Frequencies per Sector")
                .SetForm(typeof(CWindRose))
                .SetHandler(typeof(StubFileHandler))
                .SetCode("CMesoWind_FreqPerDir")
                .SetNoScroll(true)
                .AddTo(DefaultItems);

            new ProjectItemDescriptor()
                .SetDefaultName("Velocity Frequencies")
                .SetForm(typeof(CHistogram))
                .SetHandler(typeof(StubFileHandler))
                .SetCode("CMesoWind_VelFreq")
                .SetNoScroll(true)
                .AddTo(DefaultItems);

            new ProjectItemDescriptor()
                .SetDefaultName("Mean Velocity per Sector")
                .SetForm(typeof(CMeanVelPerSector))
                .SetHandler(typeof(StubFileHandler))
                .SetCode("CMesoWind_MeanVelPerDir")
                .SetNoScroll(true)
                .AddTo(DefaultItems);
        }

        public override object CreateProjectModel()
        {
            var m = new VMesoWind();
            m.Longitude = -1;
            m.Latitude = 61;
            return m;
        }
    }
}
