using System.Collections.Generic;
using System.IO;

namespace Offwind.Projects
{
    public abstract class ProjectDescriptor
    {
        public List<ProjectItemDescriptor> DefaultItems { get; private set; }
        public string Name { get; protected set; }
        public string Code { get; protected set; }
        public string Description { get; protected set; }
        public ProductType ProductType { get; protected set; }
        public CaseInitializer CaseInitializer { get; protected set; }
        public int Order { get; protected set; }
        public bool SkipStandalone { get; protected set; }

        protected ProjectDescriptor()
        {
            Order = int.MaxValue;
            DefaultItems = new List<ProjectItemDescriptor>();
            CaseInitializer = new SimpleCaseInitializer();
        }

        public virtual void InitializeProject(string projectDir)
        {
            CreateDirIfNotExist(Path.Combine(projectDir, "0.original"));
            CreateDirIfNotExist(Path.Combine(projectDir, "constant"));
            CreateDirIfNotExist(Path.Combine(projectDir, "system"));
            foreach (var item in DefaultItems)
            {
                var handler = item.CreateHandler();
                handler.WriteDefault(projectDir, null);
            }
        }

        public virtual object CreateProjectModel()
        {
            return null;
        }

        private void CreateDirIfNotExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
