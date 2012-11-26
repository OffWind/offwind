using System.IO;

namespace Offwind.Infrastructure
{
    public static class DirectoryExt
    {
        public static bool IsEmpty(string path)
        {
            int fileCount = Directory.GetFiles(path).Length;
            if (fileCount > 0)
            {
                return false;
            }

            string[] dirs = Directory.GetDirectories(path);
            foreach (string dir in dirs)
            {
                if (!IsEmpty(dir))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
