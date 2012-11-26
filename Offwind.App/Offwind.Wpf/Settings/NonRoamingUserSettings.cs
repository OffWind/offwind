using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Offwind.Common;

namespace Offwind.Settings
{
    public sealed class NonRoamingUserSettings
    {
        private const string SettingsFileName = "NonRoamingSettings.xml";
        public string SalomePath { get; set; }
        public string ParaviewPath { get; set; }
        public string ProjectsFolder { get; set; }
        public List<string> RecentProjects { get; set; }

        public string DatabaseAddress { get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseUser { get; set; }
        public string DatabasePassword { get; set; }

        public static string path
        {
            get
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                path = Path.Combine(path, Constants.ProductFolder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return Path.Combine(path, SettingsFileName);
            }
        }

        public NonRoamingUserSettings()
        {
            RecentProjects = new List<string>();
            var documentsDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            ProjectsFolder = Path.Combine(documentsDir, "Offwind Projects");

            DatabaseAddress = "sowfap01.nrg-soft.com";
            DatabaseName = "offwind";
            DatabaseUser = "offwind-worker";
            DatabasePassword = "Qwerty123";
        }

        public void Save()
        {
            using (var stream = new StreamWriter(path))
            {
                var serializer = new XmlSerializer(typeof(NonRoamingUserSettings));
                serializer.Serialize(stream, this);
            }
        }

        public NonRoamingUserSettings Read()
        {
            if (!File.Exists(path))
            {
                Save();
                return this;
            }
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var serializer = new XmlSerializer(typeof(NonRoamingUserSettings));
                var obj = (NonRoamingUserSettings)serializer.Deserialize(stream);
                this.SalomePath = obj.SalomePath;
                this.ParaviewPath = obj.ParaviewPath;
                if (NonEmpty(obj.ProjectsFolder)) this.ProjectsFolder = obj.ProjectsFolder;
                if (NonEmpty(obj.DatabaseAddress)) this.DatabaseAddress = obj.DatabaseAddress;
                if (NonEmpty(obj.DatabaseName)) this.DatabaseName = obj.DatabaseName;
                if (NonEmpty(obj.DatabaseUser)) this.DatabaseUser = obj.DatabaseUser;
                if (NonEmpty(obj.DatabasePassword)) this.DatabasePassword = obj.DatabasePassword;

                this.RecentProjects.AddRange(obj.RecentProjects);
            }
            return this;
        }

        public void Destroy()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private bool NonEmpty(string str)
        {
            return str != null && str.Trim().Length > 0;
        }
    }
}
