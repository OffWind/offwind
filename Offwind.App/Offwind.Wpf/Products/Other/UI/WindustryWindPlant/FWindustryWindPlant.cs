using System;
using System.Windows.Forms;
using Offwind.Infrastructure;
using Offwind.OpenFoam;
using Offwind.Projects;

namespace Offwind.Products.Other.UI.WindustryWindPlant
{
    public partial class FWindustryWindPlant : Form, IProjectItemView
    {
        private FoamFileHandler _fileHandler;

        public FWindustryWindPlant()
        {
            InitializeComponent();
        }

        private void tileControl1_Click(object sender, EventArgs e)
        {

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
