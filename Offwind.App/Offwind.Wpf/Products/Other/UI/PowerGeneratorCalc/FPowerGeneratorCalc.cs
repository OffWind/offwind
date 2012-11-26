using System;
using System.Windows.Forms;
using Offwind.Infrastructure;
using Offwind.OpenFoam;
using Offwind.Projects;

namespace Offwind.Products.Other.UI.PowerGeneratorCalc
{
    public partial class FPowerGeneratorCalc : Form, IProjectItemView
    {
        private FoamFileHandler _fileHandler;

        public FPowerGeneratorCalc()
        {
            InitializeComponent();
        }

        public Action GetSaveCommand()
        {
            throw new NotImplementedException();
        }

        public void SetFileHandler(FoamFileHandler handler)
        {
            _fileHandler = handler;
        }

        public void UpdateFromProject(VProject vProject)
        {
        }
    }
}
