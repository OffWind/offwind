using System.Diagnostics;
using System.Xml;

namespace Offwind.Projects.Persistence
{
    public sealed class ProjectHandler
    {
        public const string FileExtension = ".offproj";

        public static void Save(VProject vProject, XmlTextWriter xml)
        {
            Debug.Assert(vProject.ProjectDescriptor != null);
            Debug.Assert(vProject.ProjectDescriptor.Code != null);
            Debug.Assert(vProject.Items != null);

            xml.WriteStartElement("Code");
            xml.WriteValue(vProject.ProjectDescriptor.Code);
            xml.WriteEndElement();

            xml.WriteStartElement("ProjectItems");
            foreach (var item in vProject.Items)
            {
                xml.WriteStartElement("ProjectItem");

                xml.WriteStartElement("DisplayName");
                xml.WriteValue(item.DisplayName);
                xml.WriteEndElement();

                xml.WriteStartElement("Code");
                xml.WriteValue(item.Descriptor.Code);
                xml.WriteEndElement();

                xml.WriteEndElement();
            }
            xml.WriteEndElement();
        }

        public static VProject ReadFrom(XmlTextReader xml, IProjectConfiguration config)
        {
            var project = new VProject();

            if (xml.ReadToFollowing("Code"))
            {
                var code = xml.ReadElementContentAsString();
                project.ProjectDescriptor = config.GetDescriptor(code);
            }

            if (xml.ReadToFollowing("ProjectItems"))
            {
                while (xml.ReadToFollowing("ProjectItem"))
                {
                    xml.ReadToFollowing("Name");
                    var name = xml.ReadElementContentAsString();
                    xml.ReadToFollowing("Code");
                    var c = xml.ReadElementContentAsString();
                    project.Items.Add(new VProjectItem(config.GetItemDescriptor(c), name));
                }
            }
            return project;
        }
    }
}
