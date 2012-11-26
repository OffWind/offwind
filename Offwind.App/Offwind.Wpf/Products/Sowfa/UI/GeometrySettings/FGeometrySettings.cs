using System;
using System.Windows.Forms;
using Offwind.Infrastructure;
using Offwind.OpenFoam;
using Offwind.Projects;

namespace Offwind.Products.Sowfa.UI.GeometrySettings
{
    public partial class FGeometrySettings : Form, IProjectItemView
    {
        private FoamFileHandler _fileHandler;

        public FGeometrySettings()
        {
            InitializeComponent();
        }

        public Action GetSaveCommand()
        {
            return null;
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
