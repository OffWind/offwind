namespace Offwind.Projects
{
    public interface IProjectConfiguration
    {
        ProjectDescriptor GetDescriptor(string code);
        ProjectItemDescriptor GetItemDescriptor(string code);
    }
}