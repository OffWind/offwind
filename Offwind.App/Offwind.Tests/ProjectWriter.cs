using NUnit.Framework;

namespace Offwind.Tests
{
    [TestFixture]
    [Ignore]
    public class ProjectWriter
    {
        //public Project InitProject(string projectName)
        //{
        //    var vproject = new VProject();
        //    vproject.Name = projectName;
        //    vproject.ProjectDescriptor = new SowfaProject();
        //    vproject.ProjectDir = Path.Combine("c:\\temp\\", projectName);
        //    Directory.CreateDirectory(vproject.ProjectDir);
        //    vproject.Initialize();
        //    var project = vproject.GetState();
        //    //project.Name = projectName;
        //    //project.Solver = "ABL PISO Solver";
        //    //project.ProjectDir = "c:\\temp\\" + project.Name;
        //    //project.AddItem(EditorType.Constant_Gravitation.ToString(), EditorType.Constant_Gravitation);
        //    //project.AddItem(EditorType.Constant_TransportProperties.ToString(), EditorType.Constant_TransportProperties);
        //    //project.AddItem(EditorType.Constant_AblProperties.ToString(), EditorType.Constant_AblProperties);
        //    //project.AddItem(EditorType.Constant_BoundaryData.ToString(), EditorType.Constant_BoundaryData);
        //    return project;
        //}

        //[Test]
        //public void CreateNewProject()
        //{
        //    var project = InitProject(Path.GetRandomFileName().Replace(".", "_"));
        //    project.Save();
        //}

        //[Test]
        //public void LoadProject()
        //{
        //    var project = InitProject("TestProject");
        //    project.Save();

        //    var path = Path.Combine(project.ProjectDir, project.Name + Project.FileExtension);
        //    var readProject = Project.ReadFrom(path);
        //    Assert.AreEqual(project.Name, readProject.Name);
        //    Assert.AreEqual(project.Code, readProject.Code);
        //    Assert.AreEqual(project.Items.Count, readProject.Items.Count);
        //}
    }
}
