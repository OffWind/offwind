using System.Collections.Generic;

namespace Offwind.Products.OpenFoam.Models.PolyMesh
{
    public sealed class MeshBlock
    {
        public Grading grading { get; set; }
        public List<int> gradingNumbers { get; set; }
        public List<int> vertexNumbers { get; set; }
        public List<int> numberOfCells { get; set; }

        public MeshBlock()
        {
            vertexNumbers = new List<int>();
            numberOfCells = new List<int>();
            gradingNumbers = new List<int>();
        }
    }
}
