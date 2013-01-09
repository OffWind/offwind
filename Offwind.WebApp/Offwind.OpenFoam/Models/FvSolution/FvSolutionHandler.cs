using System.Collections.Generic;
using System.Text;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.FvSolution;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.OpenFoam.Models.FvSolution
{
    public sealed class FvSolutionHandler : FoamFileHandler
    {
        public FvSolutionHandler()
            : base("fvSolution", null, "system", FvSolutionRes.Default)
        {
        }

        public override object Read(string path)
        {
            var obj = new FvSolutionData();
            string txt = Load(path);

            var grammar = new OpenFoamGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(txt);

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                switch (identifier)
                {
                    case "solvers":
                        obj.Solvers = GetSolvers(rootEntryNode.GetDictContent());
                        break;
                    case "PISO":
                    case "SIMPLE":
                    case "PIMPLE":
                        obj.Solution = GetSolution(identifier, rootEntryNode.GetDictContent());
                        break;
                }
            }
            return obj;
        }

        private List<FvSolver> GetSolvers(ParseTreeNode dict)
        {
            var p = new List<FvSolver>();

            foreach (var node in dict.ChildNodes)
            {
                var solver = new FvSolver();
                var x = node.ChildNodes[0];
                solver.Name = x.GetEntryIdentifier();
                x = x.GetDictContent();
                foreach (var y in x.ChildNodes)
                {
                    var v = y.ChildNodes[0];
                    var id = v.GetEntryIdentifier();
                    switch (id)
                    {
                        case "solver":
                            solver.Solver = v.GetBasicValEnum<LinearSolver>();
                            break;
                        case "preconditioner":
                            solver.Preconditioner = v.GetBasicValEnum<Preconditioner>();
                            break;
                        case "tolerance":
                            solver.Tolerance = v.GetBasicValDecimal();
                            break;
                        case "relTol":
                            solver.RelTol = v.GetBasicValDecimal();
                            break;
                    }
                }
                p.Add(solver);
            }
            return p;
        }

        private FvSolution GetSolution(string name, ParseTreeNode entry)
        {
            var p = new FvSolution();
            p.Name = name;

            foreach (var x in entry.ChildNodes)
            {
                var y = x.ChildNodes[0];
                switch (y.GetEntryIdentifier())
                {
                    case "nCorrectors":
                        p.nCorrectors = y.GetBasicValInt();
                        break;
                    case "nNonOrthogonalCorrectors":
                        p.nNonOrthogonalCorrectors = y.GetBasicValInt();
                        break;
                    case "pRefCell":
                        p.pRefCell = y.GetBasicValDecimal();
                        break;
                    case "pRefValue":
                        p.pRefValue = y.GetBasicValDecimal();
                        break;
                }
            }
            return p;
        }

        public override void Write(string path, object data)
        {
            var obj = (FvSolutionData) data;
            var result = new StringBuilder(FvSolutionRes.Template);
            var solver = new StringBuilder();

            foreach (var s in obj.Solvers)
            {
                solver.AppendFormat("{0}{1}\n{0}{{\n" +
                                    "{0}{0}solver {2};\n" +
                                    "{0}{0}preconditioner {3};\n" +
                                    "{0}{0}tolerance {4};\n" +
                                    "{0}{0}relTol {5};\n{0}}}\n", _indent, s.Name, s.Solver, s.Preconditioner,
                                    s.Tolerance.ToString(), s.RelTol.ToString());
            }
            result.Replace("({[[solvers]]})", solver.ToString());

            result.Replace("({[[solution]]})", obj.Solution.Name);
            result.Replace("({[[nCorrectors]]})", obj.Solution.nCorrectors.ToString());
            result.Replace("({[[nNonOrthogonalCorrectors]]})", obj.Solution.nNonOrthogonalCorrectors.ToString());
            result.Replace("({[[pRefCell]]})", obj.Solution.pRefCell.ToString());
            result.Replace("({[[pRefValue]]})", obj.Solution.pRefValue.ToString());

            WriteToFile(path, result.ToString());
        }
    }
}