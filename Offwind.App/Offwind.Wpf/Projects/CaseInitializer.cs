using System.IO;
using Offwind.NewCase;

namespace Offwind.Projects
{
    public abstract class CaseInitializer
    {
        public abstract VCase Initialize(VNewCase newProject);

        protected string InitCaseDir(VNewCase newProject)
        {
            if (Directory.Exists(newProject.CaseDir)) return newProject.CaseDir;
            Directory.CreateDirectory(newProject.CaseDir);
            return newProject.CaseDir;
        }

        protected static string InitProjectDir(VNewCase newProject, string projectDir)
        {
            var dir = Path.Combine(newProject.CaseDir, projectDir);
            if (Directory.Exists(dir)) return dir;
            Directory.CreateDirectory(dir);
            return dir;
        }
    }
}
