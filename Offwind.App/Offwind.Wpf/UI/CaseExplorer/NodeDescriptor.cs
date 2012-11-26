using Offwind.Projects;

namespace Offwind.UI.CaseExplorer
{
    public sealed class NodeDescriptor
    {
        public VProjectItem ProjectItem { get; set; }
        public ImageIndex ImageIndex { get; set; }

        public NodeDescriptor()
        {
        }

        public NodeDescriptor(ImageIndex imgIndex, VProjectItem pItem)
        {
            ImageIndex = imgIndex;
            ProjectItem = pItem;
        }
    }
}
