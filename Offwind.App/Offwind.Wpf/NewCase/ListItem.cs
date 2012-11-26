using Offwind.Projects;

namespace Offwind.NewCase
{
    public sealed class ListItem
    {
        public string Group { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ProjectDescriptor Descriptor { get; set; }
    }
}
