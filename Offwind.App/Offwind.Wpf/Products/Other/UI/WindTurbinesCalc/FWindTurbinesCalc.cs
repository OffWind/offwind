using System;
using System.Windows.Forms;
using Offwind.Infrastructure;
using Offwind.OpenFoam;
using Offwind.Projects;

namespace Offwind.Products.Other.UI.WindTurbinesCalc
{
    public partial class FWindTurbinesCalc : Form, IProjectItemView
    {
        private FoamFileHandler _fileHandler;

        public FWindTurbinesCalc()
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
