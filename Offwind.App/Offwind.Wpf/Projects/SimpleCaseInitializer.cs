using Offwind.NewCase;
using Offwind.Projects.Persistence;

namespace Offwind.Projects
{
    public sealed class SimpleCaseInitializer : CaseInitializer
    {
        public override VCase Initialize(VNewCase newProject)
        {
            var vCase = new VCase();
            vCase.Name = newProject.CaseName;
            vCase.CaseDir = InitCaseDir(newProject);
            
            var pdir = InitProjectDir(newProject, newProject.ProjectDescriptor.Name);
            var proj = new VProject
            {
                DisplayName = newProject.ProjectDescriptor.Name,
                ProjectDir = pdir,
                ProjectDescriptor = newProject.ProjectDescriptor,
                ProjectModel = newProject.ProjectDescriptor.CreateProjectModel()
            };
            proj.Initialize();
            vCase.Items.Add(proj);

            CaseHandler.Write(vCase);
            return vCase;
        }
    }
}
