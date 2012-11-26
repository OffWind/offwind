using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using DevExpress.Data;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Ribbon.Customization;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;

namespace Offwind.Products.MesoWind
{
    /// <summary>
    /// Interaction logic for CImportedData.xaml
    /// </summary>
    public partial class CImportedData : UserControl, IProjectItemView
    {
        private VMesoWind _projectModel;
        private ObservableCollection<decimal[]> _data = new ObservableCollection<decimal[]>();

        public CImportedData()
        {
            InitializeComponent();
        }

        public void SetFileHandler(FoamFileHandler handler)
        {
        }

        public Action GetSaveCommand()
        {
            return null;
        }

        public void UpdateFromProject(VProject vProject)
        {
            _projectModel = (VMesoWind)vProject.ProjectModel;
            _projectModel.TargetNotified += _model_TargetNotified;

            grid.CustomUnboundColumnData += grid_CustomUnboundColumnData;

            BindData();
        }

        private void _model_TargetNotified(ProductTargets target)
        {
            BindData();
        }

        private void BindData()
        {
            _data.Clear();
            _data.Add(_projectModel.FreqByDirs.ToArray());
            _data.AddRange(_projectModel.FreqByBins);
            _data.Add(_projectModel.MeanVelocityPerDir.ToArray());

            grid.Columns.Clear();
            var titleCol = new GridColumn();
            titleCol.UnboundType = UnboundColumnType.String;
            titleCol.ReadOnly = true;
            titleCol.FieldName = String.Format("Title");
            titleCol.Header = "#";
            titleCol.Width = 70;
            titleCol.Tag = -1;
            grid.Columns.Add(titleCol);

            for (var dirIdx = 0; dirIdx < _projectModel.NDirs; dirIdx++)
            {
                var col = new GridColumn();
                col.UnboundType = UnboundColumnType.Decimal;
                col.ReadOnly = true;
                col.FieldName = String.Format("ColumnN{0}", dirIdx);
                col.Header = String.Format("{0}", (360/_projectModel.NDirs)*dirIdx);
                col.Tag = dirIdx;
                col.Width = 70;
                grid.Columns.Add(col);
            }

            grid.ItemsSource = null;
            grid.ItemsSource = _data;
        }

        void grid_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                var dirIdx = (int) e.Column.Tag;
                var row = e.ListSourceRowIndex;
                if (dirIdx == -1)
                {
                    e.Value = row;
                    if (row == 0)
                    {
                        e.Value = "Frequency";
                    }
                    else if (row == _data.Count - 1)
                    {
                        e.Value = "Mean Vel.";
                    }
                }
                else
                {
                    e.Value = _data[row][dirIdx];
                }
            }
        }
    }
}
