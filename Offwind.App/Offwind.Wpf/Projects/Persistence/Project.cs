using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace Offwind.Projects.Persistence
{
    public sealed class Project
    {
        public const string FileExtension = ".offproj";

        public Guid Id { get; set; }
        public string ProjectDir { get; set; }
        public string Code { get; set; }
        public List<ProjectItem> Items { get; set; }

        public Project()
        {
            Items = new List<ProjectItem>();
        }

        public void Save()
        {
            var projectPath = Path.Combine(ProjectDir, Name + FileExtension);
            using (var xml = new XmlTextWriter(projectPath, Encoding.UTF8))
            {
                Debug.Assert(Id != Guid.Empty);
                Debug.Assert(Name != null);
                Debug.Assert(Code != null);
                Debug.Assert(Items != null);

                xml.Formatting = Formatting.Indented;

                xml.WriteStartDocument();
                xml.WriteStartElement("Project");

                xml.WriteStartElement("Id");
                xml.WriteValue(Id.ToString());
                xml.WriteEndElement();

                xml.WriteStartElement("Name");
                xml.WriteValue(Name);
                xml.WriteEndElement();

                xml.WriteStartElement("Code");
                xml.WriteValue(Code);
                xml.WriteEndElement();

                xml.WriteStartElement("Items");
                foreach (var item in Items)
                {
                    xml.WriteStartElement("Item");

                    xml.WriteStartElement("Name");
                    xml.WriteValue(item.Name);
                    xml.WriteEndElement();

                    xml.WriteStartElement("Type");
                    xml.WriteValue(item.Type);
                    xml.WriteEndElement();

                    xml.WriteEndElement();
                }
                xml.WriteEndElement();

                xml.WriteEndElement(); // Project
                xml.WriteEndDocument();
            }
        }

        public static Project ReadFrom(string filePath)
        {
            var project = new Project();

            using (var stream = new StreamReader(filePath, Encoding.UTF8))
            using (var xml = new XmlTextReader(stream))
            {
                xml.ReadToFollowing("Project");

                if (xml.ReadToFollowing("Id"))
                {
                    project.Id = new Guid(xml.ReadElementContentAsString());
                }

                if (xml.ReadToFollowing("Name"))
                {
                    project.Name = xml.ReadElementContentAsString();
                }

                if (xml.ReadToFollowing("Code"))
                {
                    project.Code = xml.ReadElementContentAsString();
                }

                if (xml.ReadToFollowing("Items"))
                {
                    while (xml.ReadToFollowing("Item"))
                    {
                        xml.ReadToFollowing("Name");
                        var name = xml.ReadElementContentAsString();
                        xml.ReadToFollowing("Type");
                        var et = xml.ReadElementContentAsString();
                        project.Items.Add(new ProjectItem { Name = name, Type = et });
                    }
                }
            }
            return project;
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
