using System.Collections.Generic;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.Sowfa.System.RefineMeshDict
{
    public enum CoordinateSystem
    {
        global,
        patchLocal
    }

    public enum DirectionType
    {
        tan1, tan2, normal
    }

    public struct Coeffs
    {
        public DirectionType dir { set; get; }
        public Vertice value { set; get; }
    }

    public sealed class RefineMeshDictData
    {
        public string patch { set; get; }
        public string setvalue { set; get; }
        public CoordinateSystem coordsys { set; get; }
        public List<Coeffs> globalCoeffs { set; get; }
        public List<Coeffs> patchLocalCoeffs { set; get; } 
        public List<DirectionType> direction { set; get; } 
        public bool useHexTopology { set; get; }
        public bool geometricCut { set; get; }
        public bool writeMesh { set; get; }
    }
}
