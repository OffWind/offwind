using System.Collections.ObjectModel;

namespace Offwind.Projects
{
    public sealed class VProject : VCaseItem
    {
        public ProjectDescriptor ProjectDescriptor { get; set; }
        public string ProjectDir { get; set; }
        public ObservableCollection<VProjectItem> Items { get; set; }
        public object ProjectModel { get; set; }

        public VProject()
        {
            Items = new ObservableCollection<VProjectItem>();
        }

        public void Initialize()
        {
            ProjectDescriptor.InitializeProject(ProjectDir);
            foreach (var descriptor in ProjectDescriptor.DefaultItems)
            {
                Items.Add(new VProjectItem(descriptor, descriptor.DefaultName));
            }
        }

        public bool IsReady()
        {
            if (this.Items.Count == 0) return false;
            return true;
        }
    }
}
