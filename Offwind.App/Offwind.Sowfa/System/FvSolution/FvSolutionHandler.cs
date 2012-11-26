using System.Diagnostics;
using System.IO;
using System.Text;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.FvSolution;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.System.FvSolution
{
    public sealed class FvSolutionHandler : FoamFileHandler
    {
        public FvSolutionHandler()
            : base("fvSolution", null, "system", FvSolutionRes.Default)
        {
        }

        public override object Read(string path)
        {
            var rawData = new FvSolutionData();
            string txt;
            using (var reader = new StreamReader(path))
            {
                txt = reader.ReadToEnd();
            }

            var grammar = new OpenFoamGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(txt);

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                switch (identifier)
                {
                    case "options":
                        ParseOptions(rootEntryNode.ChildNodes[2], rawData);
                        break;
                    case "solvers":
                        ParseSolvers(rootEntryNode.ChildNodes[2], rawData);
                        break;
                }
            }
            return rawData;
        }

        private void ParseOptions(ParseTreeNode node, FvSolutionData rawData)
        {
            foreach (ParseTreeNode rootEntryNode in node.FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                switch (identifier)
                {
                    case "nCorrectors":
                        rawData.Options.nCorrectors = rootEntryNode.GetBasicValInt();
                        break;
                    case "nNonOrthogonalCorrectors":
                        rawData.Options.nNonOrthogonalCorrectors = rootEntryNode.GetBasicValInt();
                        break;
                    case "pdRefOn":
                        rawData.Options.pdRefOn = rootEntryNode.GetBasicValBool();
                        break;
                    case "pdRefCell":
                        rawData.Options.pdRefCell = rootEntryNode.GetBasicValInt();
                        break;
                    case "pdRefValue":
                        rawData.Options.pdRefValue = rootEntryNode.GetBasicValInt();
                        break;
                    case "tempEqnOn":
                        rawData.Options.tempEqnOn = rootEntryNode.GetBasicValBool();
                        break;
                }
            }
        }

        private void ParseSolvers(ParseTreeNode node, FvSolutionData rawData)
        {
            foreach (ParseTreeNode rootEntryNode in node.FindDictEntries(null))
            {
                var s = new MLinearSolver();
                s.Name = rootEntryNode.GetEntryIdentifier();
                rawData.Solvers.Add(s);
                ParseSolver(rootEntryNode.ChildNodes[2], s);
            }
        }

        private void ParseSolver(ParseTreeNode node, MLinearSolver s)
        {
            foreach (ParseTreeNode rootEntryNode in node.FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                switch (identifier)
                {
                    case "solver":
                        s.solver = rootEntryNode.GetBasicValEnum<LinearSolver>();
                        break;
                    case "smoother":
                        s.smoother = rootEntryNode.GetBasicValEnum<Smoother>();
                        break;
                    case "agglomerator":
                        s.agglomerator = rootEntryNode.GetBasicValEnum<Agglomerator>();
                        break;
                    case "tolerance":
                        s.tolerance = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "relTol":
                        s.relTol = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "nPreSweeps":
                        s.nPreSweeps = rootEntryNode.GetBasicValInt();
                        break;
                    case "nPostSweeps":
                        s.nPostSweeps = rootEntryNode.GetBasicValInt();
                        break;
                    case "nFinestSweeps":
                        s.nFinestSweeps = rootEntryNode.GetBasicValInt();
                        break;
                    case "cacheAgglomeration":
                        s.cacheAgglomeration = rootEntryNode.GetBasicValBool();
                        break;
                    case "nCellsInCoarsestLevel":
                        s.nCellsInCoarsestLevel = rootEntryNode.GetBasicValInt();
                        break;
                    case "mergeLevels":
                        s.mergeLevels = rootEntryNode.GetBasicValInt();
                        break;
                }
            }
        }

        public override void Write(string path, object data)
        {
            var d = (FvSolutionData)data;
            var t = new StringBuilder(FvSolutionRes.Template);
            t.Replace("({[[nCorrectors]]})", d.Options.nCorrectors.ToString());
            t.Replace("({[[nNonOrthogonalCorrectors]]})", d.Options.nNonOrthogonalCorrectors.ToString());
            t.Replace("({[[pdRefOn]]})", d.Options.pdRefOn.ToString().ToLowerInvariant());
            t.Replace("({[[pdRefCell]]})", d.Options.pdRefCell.ToString());
            t.Replace("({[[pdRefValue]]})", d.Options.pdRefValue.ToString());
            t.Replace("({[[tempEqnOn]]})", d.Options.tempEqnOn.ToString().ToLowerInvariant());

            var tSolvers = new StringBuilder();
            foreach (var solver in d.Solvers)
            {
                Debug.Assert(Validator.IsIdentifier(solver.Name), "Incorrect linear solver name");

                var ts = new StringBuilder();
                ts.Append(FvSolutionRes.TemplateSolver);
                ts.Replace("({[[name]]})", solver.Name.ToString());
                ts.Replace("({[[solver]]})", solver.solver.ToString());
                ts.Replace("({[[tolerance]]})", solver.tolerance.ToString());
                ts.Replace("({[[relTol]]})", solver.relTol.ToString());
                ts.Replace("({[[smoother]]})", solver.smoother.ToString());
                ts.Replace("({[[nPreSweeps]]})", solver.nPreSweeps.ToString());
                ts.Replace("({[[nPostSweeps]]})", solver.nPostSweeps.ToString());
                ts.Replace("({[[nFinestSweeps]]})", solver.nFinestSweeps.ToString());
                ts.Replace("({[[cacheAgglomeration]]})", solver.cacheAgglomeration.ToString().ToLowerInvariant());
                ts.Replace("({[[nCellsInCoarsestLevel]]})", solver.nCellsInCoarsestLevel.ToString());
                ts.Replace("({[[agglomerator]]})", solver.agglomerator.ToString());
                ts.Replace("({[[mergeLevels]]})", solver.mergeLevels.ToString());
                tSolvers.Append(ts);
            }

            t.Replace("({[[solvers]]})", tSolvers.ToString());
            WriteToFile(path, t.ToString());
        }
    }
}
