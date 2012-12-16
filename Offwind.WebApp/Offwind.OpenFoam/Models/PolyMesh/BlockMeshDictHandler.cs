using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Irony.Parsing;
using Offwind.OpenFoam.Models.PolyMesh;
using Offwind.Products.OpenFoam.Models.Fields;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Products.OpenFoam.Models.PolyMesh
{
    public sealed class BlockMeshDictHandler : FoamFileHandler
    {
        public BlockMeshDictHandler()
            : base("blockMeshDict", null, "constant\\polyMesh", BlockMeshDictRes.Default)
        {
        }

        public override object Read(string path)
        {
            var rawData = new BlockMeshDictData();
            string txt = Load(path);

            var grammar = new OpenFoamGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(txt);

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                switch (identifier)
                {
                    case "convertToMeters":
                        rawData.convertToMeters = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "vertices":
                        var vertices = rootEntryNode.GetDictArrayBody().GetVectorsArray();
                        rawData.vertices.AddRange(vertices);
                        break;
                    case "blocks":
                        var blocks = rootEntryNode.GetDictArrayBody().GetArray(n => n);
                        rawData.MeshBlocks.vertexNumbers.AddRange(blocks[1].ChildNodes[1].GetArrayOfInt32());
                        rawData.MeshBlocks.numberOfCells.AddRange(blocks[2].ChildNodes[1].GetArrayOfInt32());
                        rawData.MeshBlocks.gradingNumbers.AddRange(blocks[4].ChildNodes[1].GetArrayOfInt32());
                        
                        var gradingText = blocks[3].Token.Text;
                        rawData.MeshBlocks.grading = (Grading)Enum.Parse(typeof(Grading), gradingText);
                        break;
                    case "boundary":
                        var boundaries = rootEntryNode.GetDictArrayBody().GetArray(ParseBoundary);
                        rawData.boundaries.AddRange(boundaries);
                        break;
                }
            }
            return rawData;
        }

        private MeshBoundary ParseBoundary(ParseTreeNode node)
        {
            var b = new MeshBoundary();
            b.Name = node.ChildNodes[0].Token.Text;
            foreach (ParseTreeNode rootEntryNode in node.ChildNodes[2].FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                switch (identifier)
                {
                    case "type":
                        b.Type = rootEntryNode.GetBasicValEnum<PatchType>();
                        break;
                    case "neighbourPatch":
                        b.NeighbourPatch = rootEntryNode.GetBasicValString();
                        break;
                    case "faces":
                        b.Faces = new List<List<int>>();
                        var arrayBody = rootEntryNode.GetDictArrayBody();
                        Func<ParseTreeNode, List<int>> fconv = nn =>
                        {
                            var r = new List<int>();
                            r.AddRange(nn.ChildNodes[1].GetArrayOfInt32());
                            return r;
                        };
                        b.Faces.AddRange(arrayBody.GetArray(fconv));
                        break;
                }
            }
            return b;
        }

        public override void Write(string path, object data)
        {
            var d = (BlockMeshDictData)data;
            var t = new StringBuilder(BlockMeshDictRes.Template);
            t.Replace("({[[convertToMeters]]})", d.convertToMeters.ToString());
            var vt = new StringBuilder();
            foreach (var vertice in d.vertices)
            {
                vt.Append("    ");
                vt.Append(vertice.WriteVector());
                vt.AppendLine();
            }

            t.Replace("({[[vertices]]})", vt.ToString());

            Debug.Assert(d.MeshBlocks.vertexNumbers.Count > 0);
            Debug.Assert(d.MeshBlocks.numberOfCells.Count > 0);
            Debug.Assert(d.MeshBlocks.gradingNumbers.Count > 0);
            
            t.Replace("({[[vertexNumbers]]})", d.MeshBlocks.vertexNumbers.WriteArray());
            t.Replace("({[[numberOfCells]]})", d.MeshBlocks.numberOfCells.WriteArray());
            t.Replace("({[[grading]]})", d.MeshBlocks.grading.ToString());
            t.Replace("({[[gradingNumbers]]})", d.MeshBlocks.gradingNumbers.WriteArray());

            var bst = new StringBuilder();
            foreach (var bb in d.boundaries)
            {
                var bt = new StringBuilder(BlockMeshDictRes.TemplateBoundary);
                bt.Replace("({[[name]]})", bb.Name);
                bt.Replace("({[[patchType]]})", bb.Type.ToString());
                if (bb.NeighbourPatch != null)
                {
                    var s = String.Format("neighbourPatch    {0};", bb.NeighbourPatch);
                    bt.Replace("({[[neighbourPatch]]})", s);
                }
                else
                {
                    bt.Replace("({[[neighbourPatch]]})", bb.NeighbourPatch);
                }

                var ft = new StringBuilder();
                foreach (var face in bb.Faces)
                {
                    ft.Append("            ");
                    ft.AppendLine(face.WriteArray());
                }
                bt.Replace("({[[faces]]})", ft.ToString());
                bst.Append(bt);
                bst.AppendLine();
            }
            t.Replace("({[[boundaries]]})", bst.ToString());

            WriteToFile(path, t.ToString());
        }
    }
}
