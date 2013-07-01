using System;

namespace Offwind.WebApp.Models
{
    public class VFile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Uploaded { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
    }
}