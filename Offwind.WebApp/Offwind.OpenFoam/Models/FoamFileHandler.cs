using System.Diagnostics;
using System.IO;
using System.Text;

namespace Offwind.Products.OpenFoam.Models
{
    /// <summary>
    /// General interface for reading/writing OpenFOAM input files.
    /// Data type "object" is for the sake of simplicity.
    /// </summary>
    public abstract class FoamFileHandler
    {
        public string FileName { get; set; }
        public string FileSuffix { get; set; }
        public string RelativePath { get; set; }
        public string DefaultData { get; set; }

        protected FoamFileHandler()
        {
            FileName = "stub";
            RelativePath = "";
            DefaultData = "";
            FileSuffix = null;
        }

        protected FoamFileHandler(string fileName, string fileSuffix, string relativePath, string defaultData)
        {
            FileName = fileName;
            RelativePath = relativePath;
            DefaultData = defaultData;
            FileSuffix = fileSuffix;
        }

        public abstract object Read(string path);
        public abstract void Write(string path, object data);

        protected void WriteToFile(string path, string data)
        {
            Debug.Write(data);
            using (var w = new StreamWriter(path, false, Encoding.ASCII))
            {
                w.Write(data);
            }
        }

        protected void CreateIfNotExist(string dir)
        {
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }

        public string GetPath(string solverDir)
        {
            string dir;
            if (RelativePath != null && RelativePath.Trim().Length > 0)
            {
                dir = Path.Combine(solverDir, RelativePath);
            }
            else
            {
                dir = solverDir;
            }
            CreateIfNotExist(dir);
            if (FileSuffix != null && FileSuffix.Length > 0)
                return Path.Combine(dir, FileName + "." + FileSuffix);

            return Path.Combine(dir, FileName);
        }

        public virtual void WriteDefault(string solverDir, object data)
        {
            Debug.WriteLine(this.GetType().Name);
            var path = GetPath(solverDir);
            if (data == null)
            {
                WriteToFile(path, DefaultData);
            }
            else
            {
                Write(path, data);
            }
        }

        public string Load(string path)
        {
            if (path == null)
            {
                return DefaultData;
            }
            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
