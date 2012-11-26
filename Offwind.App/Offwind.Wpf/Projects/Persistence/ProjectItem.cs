namespace Offwind.Projects.Persistence
{
    public sealed class ProjectItem
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Type, Name);
        }
    }
}
