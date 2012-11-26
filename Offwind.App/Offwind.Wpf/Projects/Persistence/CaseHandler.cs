using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace Offwind.Projects.Persistence
{
    public sealed class CaseHandler
    {
        public const string FileExtension = ".offcase";
        public const string ProjectItem = "Project";

        public static void Write(VCase vCase)
        {
            var path = Path.Combine(vCase.CaseDir, vCase.Name + FileExtension);
            using (var xml = new XmlTextWriter(path, Encoding.UTF8))
            {
                xml.Formatting = Formatting.Indented;
                xml.WriteStartDocument();
                xml.WriteStartElement("Case");

                xml.WriteStartElement("Name");
                xml.WriteValue(vCase.Name);
                xml.WriteEndElement();

                xml.WriteStartElement("CaseItems");
                foreach (var item in vCase.Items)
                {
                    xml.WriteStartElement("CaseItem");

                    xml.WriteStartElement("Id");
                    xml.WriteValue(item.Id.ToString());
                    xml.WriteEndElement();

                    xml.WriteStartElement("DisplayName");
                    xml.WriteValue(item.DisplayName);
                    xml.WriteEndElement();

                    xml.WriteStartElement("RelativePath");
                    xml.WriteValue(item.RelativePath ?? "");
                    xml.WriteEndElement();

                    var vProject = item as VProject;
                    if (vProject != null)
                    {
                        xml.WriteStartElement("Type");
                        xml.WriteValue(ProjectItem);
                        xml.WriteEndElement();

                        ProjectHandler.Save(vProject, xml);
                    }

                    xml.WriteEndElement();
                }
                xml.WriteEndElement();

                xml.WriteEndElement(); // Case
                xml.WriteEndDocument();
            }
        }

        public static VCase ReadFrom(string filePath, IProjectConfiguration config)
        {
            var vCase = new VCase();

            using (var stream = new StreamReader(filePath, Encoding.UTF8))
            using (var xml = new XmlTextReader(stream))
            {
                xml.ReadToFollowing("Case");

                if (xml.ReadToFollowing("Name"))
                {
                    vCase.Name = xml.ReadElementContentAsString();
                }

                if (xml.ReadToFollowing("CaseItems"))
                {
                    while (xml.ReadToFollowing("CaseItem"))
                    {
                        VCaseItem item = null;
                        xml.ReadToFollowing("Id");
                        var id = xml.ReadElementContentAsString();

                        xml.ReadToFollowing("DisplayName");
                        var displayName = xml.ReadElementContentAsString();

                        xml.ReadToFollowing("RelativePath");
                        var relativePath = xml.ReadElementContentAsString();

                        xml.ReadToFollowing("Type");
                        var type = xml.ReadElementContentAsString();
                        if (type == ProjectItem)
                        {
                            item = ProjectHandler.ReadFrom(xml, config);
                        }

                        Debug.Assert(item != null);
                        item.Id = new Guid(id);
                        item.DisplayName = displayName;
                        item.RelativePath = relativePath;
                        vCase.Items.Add(item);
                    }
                }
            }
            return vCase;
        }
    }
}
