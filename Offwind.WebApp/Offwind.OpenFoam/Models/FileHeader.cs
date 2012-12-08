namespace Offwind.Products.OpenFoam.Models
{
    public sealed class FileHeader
    {
        public string Version { get; set; }
        public Format Format { get; set; }
        public string Location { get; set; }
        public string Class { get; set; }
        public string Filename { get; set; }
    }
}
