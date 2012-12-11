using System.IO;

namespace Offwind.WebApp.Models.Jobs
{
    public enum JobResult
    {
        None,
        Ok,
        Error
    }

    class MyClass
    {
         void GetFileName()
         {
             var path = Path.GetTempFileName();
             var fileName = Path.GetFileName(path);
         }
    }
}