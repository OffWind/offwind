namespace Offwind.Products.OpenFoam.Models
{
    public sealed class StubFileHandler : FoamFileHandler
    {
        public override object Read(string path)
        {
            return null;
        }

        public override void Write(string path, object data)
        {
        }

        public override void WriteDefault(string solverDir, object data)
        {
            // Do nothing
        }
    }
}
