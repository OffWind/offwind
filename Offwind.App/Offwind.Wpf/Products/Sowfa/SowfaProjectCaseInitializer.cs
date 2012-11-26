using Offwind.NewCase;
using Offwind.Products.MesoWind;
using Offwind.Products.WindWave;
using Offwind.Projects;
using Offwind.Projects.Persistence;

namespace Offwind.Products.Sowfa
{
    public sealed class SowfaProjectCaseInitializer : CaseInitializer
    {
        public override VCase Initialize(VNewCase newProject)
        {
            var vCase = new VCase();
            vCase.Name = newProject.CaseName;
            vCase.CaseDir = InitCaseDir(newProject);

            InitSowfaNormal(newProject, vCase);
            //InitAblPisoSolver(newProject, vCase);
            //InitWindPlant(newProject, vCase);
            InitMesoWind(newProject, vCase);
            InitWindWave(newProject, vCase);

            CaseHandler.Write(vCase);
            return vCase;
        }

        private static void InitSowfaNormal(VNewCase newProject, VCase vCase)
        {
            var d = new SowfaNormal();
            var pdir = InitProjectDir(newProject, d.Name);
            var proj = new VProject
            {
                DisplayName = d.Name,
                ProjectDir = pdir,
                ProjectDescriptor = d,
                ProjectModel = d.CreateProjectModel()
            };
            proj.Initialize();
            vCase.Items.Add(proj);
        }

        //private static void InitAblPisoSolver(VNewCase newProject, VCase vCase)
        //{
        //    var d = new AblPisoSolver();
        //    var pdir = InitProjectDir(newProject, d.Name);
        //    var proj = new VProject
        //    {
        //        DisplayName = d.Name,
        //        ProjectDir = pdir,
        //        ProjectDescriptor = d,
        //    };
        //    proj.Initialize();
        //    vCase.Items.Add(proj);
        //}

        //private static void InitAblGeometryAndMesh(VNewProject newProject, VCase vCase)
        //{
        //    var d = new AblGeometryAndMesh();
        //    var pdir = InitProjectDir(newProject, d.Name);
        //    var proj = new VProject
        //    {
        //        Name = d.Name,
        //        ProjectDir = pdir,
        //        ProjectDescriptor = d,
        //    };
        //    proj.Initialize();
        //    proj.GetState().Save();
        //    vCase.Items.Add(proj);
        //}

        //private static void InitWindPlant(VNewCase newProject, VCase vCase)
        //{
        //    var d = new WindPlantPisoSolver();
        //    var pdir = InitProjectDir(newProject, d.Name);
        //    var proj = new VProject
        //    {
        //        DisplayName = d.Name,
        //        ProjectDir = pdir,
        //        ProjectDescriptor = d,
        //    };
        //    proj.Initialize();
        //    vCase.Items.Add(proj);
        //}

        private static void InitMesoWind(VNewCase newProject, VCase vCase)
        {
            var d = new MesoWindProjectDescriptor();
            var pdir = InitProjectDir(newProject, d.Name);
            var proj = new VProject
            {
                DisplayName = d.Name,
                ProjectDir = pdir,
                ProjectDescriptor = d,
                ProjectModel = d.CreateProjectModel()
            };
            proj.Initialize();
            vCase.Items.Add(proj);
        }

        private static void InitWindWave(VNewCase newProject, VCase vCase)
        {
            var d = new WindWaveProjectDescriptor();
            var pdir = InitProjectDir(newProject, d.Name);
            var proj = new VProject
            {
                DisplayName = d.Name,
                ProjectDir = pdir,
                ProjectDescriptor = d,
                ProjectModel = d.CreateProjectModel()
            };
            proj.Initialize();
            vCase.Items.Add(proj);
        }
    }
}
