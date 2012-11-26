using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Offwind.Infrastructure;
using Offwind.OpenFoam;
using Offwind.Projects;

namespace Offwind.UI.fvSolution
{
    public partial class FFvSolution : Form, IProjectItemView
    {
        private readonly VFvSolution _model = new VFvSolution();
        private readonly ModelChangedIndicator _modelChangedIndicator;
        private FoamFileHandler _fileHandler;
        private VProject _vProject;

        public FFvSolution()
        {
            InitializeComponent();

            _modelChangedIndicator = new ModelChangedIndicator(this);
            _model.ModelChanged += _modelChangedIndicator.ModelChangedHandler;
        }

        public Action GetSaveCommand()
        {
            throw new NotImplementedException();
        }

        public void SetFileHandler(FoamFileHandler handler)
        {
            throw new NotImplementedException();
        }

        public void UpdateFromProject(VProject vProject)
        {
            throw new NotImplementedException();
        }
    }
}
