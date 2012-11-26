using System;
using DevExpress.Xpf.Grid;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;
using Offwind.Sowfa.System.FvSchemes;

namespace Offwind.Products.OpenFoam.UI.fvSchemes
{
    /// <summary>
    /// Interaction logic for CFvScheme.xaml
    /// </summary>
    public partial class CFvScheme : IProjectItemView
    {
        private VProject _vProject;
        private FoamFileHandler _fileHandler;

        private readonly VSchemesCollection schemes;

        private readonly FvSchemesData test = new FvSchemesData( true );

        public CFvScheme()
        {
            InitializeComponent();
            schemes = new VSchemesCollection();
            //InterpolationTable.CellValueChanged += new CellValueChangedEventHandler(CellValueChanged);
            DataContext = schemes;
        }

        public Action GetSaveCommand()
        {
            return () =>
                       {
                           var path = _fileHandler.GetPath(_vProject.ProjectDir);
                           var d = new FvSchemesData(true);

                           foreach (var x in schemes.cInterpolation)
                           {
                                d.interpolationSchemes.Add(new InterpolationScheme()
                                {
                                    scheme = x.Scheme,
                                    use_default = (x.Scheme == "default"),
                                    function = x.Function,
                                    interpolation = x.InterpolationType,
                                    view = x.BoundView,
                                    lower_limit = (x.BoundView != BoundView.None) ? x.LowerLimit : 0,
                                    upper_limit = (x.BoundView != BoundView.None) ? x.UpperLimit : 0,
                                    flux = x.Flux,
                                    psi = x.Psi
                                });
                           }
                           foreach (var x in schemes.cSnGrad)
                           {
                                d.snGradSchemes.Add(new SurfaceNormalGradientScheme()
                                {
                                    scheme = x.Scheme,
                                    use_default = (x.Scheme == "default"),
                                    function = x.Function,
                                    type = x.SurfaceNoramGradientType,
                                    psi = x.Psi
                                });
                           }
                           foreach (var x in schemes.cGradient)
                           {
                                d.gradSchemes.Add(new GradientScheme()
                                {
                                    scheme = x.Scheme,
                                    use_default = (x.Scheme == "default"),
                                    function = x.Function,
                                    interpolation = x.InterpolationType,
                                    discretisation = x.DiscretisationType,
                                    limited = x.LimitedType,
                                    psi = x.Psi
                                });
                           }
                           foreach (var x in schemes.cDivergence)
                           {
                                d.divSchemes.Add(new DivergenceScheme()
                                {
                                    scheme = x.Scheme,
                                    use_default = (x.Scheme == "default"),
                                    function = x.Function,
                                    discretisation = x.DiscretisationType,
                                    interpolation = x.InterpolationType,
                                    view = x.BoundView,
                                    lower_limit = (x.BoundView != BoundView.None) ? x.LowerLimit : 0,
                                    upper_limit = (x.BoundView != BoundView.None) ? x.UpperLimit : 0,
                                    psi = x.Psi
                                });
                           }
                           foreach (var x in schemes.cLaplacian)
                           {
                                d.laplacianSchemes.Add(new LaplacianScheme()
                                {
                                    scheme = x.Scheme,
                                    use_default = (x.Scheme == "default"),
                                    function = x.Function,
                                    interpolation = x.InterpolationType,
                                    discretisation = x.DiscretisationType,
                                    snGradScheme = x.SurfaceNoramGradientType,
                                    psi = x.Psi
                                });
                           }
                           foreach (var x in schemes.cTime)
                           {
                                d.ddtSchemes.Add(new TimeScheme()
                                {
                                    scheme = x.Scheme,
                                    use_default = (x.Scheme == "default"),
                                    function = x.Function,
                                    type = x.TimeSchemeType,
                                    psi = x.Psi
                                });
                           }
                           foreach (var x in schemes.cFlux)
                           {
                                d.fluxCalculation.Add(new FluxCalculation()
                                {
                                    flux = x.Flux,
                                    enable = x.Enable
                                });
                           }
                           _fileHandler.Write(path, d);
                           schemes.AcceptChanges();
                       };
        }

        public void SetFileHandler(FoamFileHandler handler)
        {
            _fileHandler = handler;
        }

        public void UpdateFromProject(VProject vProject)
        {
            _vProject = vProject;
            var path = _fileHandler.GetPath(_vProject.ProjectDir);
            var d = (FvSchemesData)_fileHandler.Read(path);

            foreach (var x in d.interpolationSchemes)
            {
                schemes.cInterpolation.Add(new VInterpolationScheme()
                {
                    Scheme = (x.use_default) ? "default" : x.scheme,
                    Function = (x.use_default) ? "" : x.function,
                    BoundView = x.view,
                    Flux = x.flux,
                    InterpolationType = x.interpolation,
                    LowerLimit = (x.view != BoundView.None) ? x.lower_limit : 0,
                    UpperLimit = (x.view != BoundView.None) ? x.upper_limit : 0,
                    Psi = x.psi
                });
            }
            foreach (var x in d.snGradSchemes)
            {
                schemes.cSnGrad.Add(new VSurfaceNormalGradientScheme()
                {
                    Scheme = (x.use_default) ? "default" : x.scheme,
                    Function = (x.use_default) ? "" : x.function,
                    SurfaceNoramGradientType = x.type,
                    Psi = x.psi
                });
            }
            foreach (var x in d.gradSchemes)
            {
                schemes.cGradient.Add(new VGradientScheme()
                {
                    Scheme = (x.use_default) ? "default" : x.scheme,
                    Function = (x.use_default) ? "" : x.function,
                    InterpolationType = x.interpolation,
                    DiscretisationType = x.discretisation,
                    LimitedType = x.limited,
                    Psi = x.psi
                });
            }
            foreach (var x in d.divSchemes)
            {
                schemes.cDivergence.Add(new VDivergenceScheme()
                {
                    Scheme = (x.use_default) ? "default" : x.scheme,
                    Function = (x.use_default) ? "" : x.function,
                    DiscretisationType = x.discretisation,
                    BoundView = x.view,
                    InterpolationType = x.interpolation,
                    LowerLimit = (x.view != BoundView.None) ? x.lower_limit : 0,
                    UpperLimit = (x.view != BoundView.None) ? x.upper_limit : 0,
                    Psi = x.psi                                                                       
                });
            }
            foreach (var x in d.laplacianSchemes)
            {
                schemes.cLaplacian.Add(new VLaplacianScheme()
                {
                    Scheme = (x.use_default) ? "default" : x.scheme,
                    Function = (x.use_default) ? "" : x.function,
                    InterpolationType = x.interpolation,
                    DiscretisationType = x.discretisation,
                    SurfaceNoramGradientType = x.snGradScheme,
                    Psi = x.psi
                });
            }
            foreach (var x in d.ddtSchemes)
            {
                schemes.cTime.Add(new VTimeScheme()
                {
                    Scheme = (x.use_default) ? "default" : x.scheme,
                    Function = (x.use_default) ? "" : x.function,
                    TimeSchemeType = x.type,
                    Psi = x.psi
                });
            }
            foreach (var x in d.fluxCalculation)
            {
                schemes.cFlux.Add(new VFluxControl()
                {
                    Flux = x.flux,
                    Enable = x.enable
                });
            }
            schemes.AcceptChanges();
        }     

        private void CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var row = (VInterpolationScheme) e.Row;

            switch (e.Column.Name)
            {
                case "IScheme":
                    if ( row.Scheme == "default")
                    {
                        //InterpolationGrid.SetCellValue(e.RowHandle, InterpolationGrid.Columns["Scheme"], "");                        
                        var obj = InterpolationGrid.View.GetCellElementByRowHandleAndColumn(e.RowHandle,
                                                                                  InterpolationGrid.Columns["Function"]);
                    }
                    break;
                case "IBounded":
                    {
                        if ( row.BoundView != BoundView.Range)
                        {
                            InterpolationGrid.SetCellValue(e.RowHandle, InterpolationGrid.Columns["LowerLimit"], 0);
                            InterpolationGrid.SetCellValue(e.RowHandle, InterpolationGrid.Columns["UpperLimit"], 0);                           
                        }
                    }
                    break;
            }
        }

        private void bbNew_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            var active_tab = NumericalShemes.SelectedTabItem;

            switch (active_tab.Name)
            {
                case "Interpolation":
                    schemes.cInterpolation.Add(new VInterpolationScheme());
                    break;
                case "SnGrad":
                    schemes.cSnGrad.Add(new VSurfaceNormalGradientScheme());
                    break;
                case "Divergence":
                    schemes.cDivergence.Add(new VDivergenceScheme());
                    break;
                case "Gradient":
                    schemes.cGradient.Add(new VGradientScheme());
                    break;
                case "Laplacian":
                    schemes.cLaplacian.Add(new VLaplacianScheme());
                    break;
                case "TimeScheme":
                    schemes.cTime.Add(new VTimeScheme());
                    break;
                default:
                    schemes.cFlux.Add(new VFluxControl());
                    break;
            }
        }

        private void bbDelete_ItemClick( object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e )
        {
            var active_tab = NumericalShemes.SelectedTabItem;
            switch (active_tab.Name)
            {
                case "Interpolation":
                    {
                        if (schemes.cInterpolation.Count > 0)
                        {
                            var row = (VInterpolationScheme) InterpolationGrid.GetFocusedRow();
                            schemes.cInterpolation.Remove(row);
                        }
                    }
                    break;
                case "SnGrad":
                    {
                        if (schemes.cSnGrad.Count > 0)
                        {
                            var row = (VSurfaceNormalGradientScheme) SnGradGrid.GetFocusedRow();
                            schemes.cSnGrad.Remove(row);
                        }
                    }
                    break;
                case "Divergence":
                    {
                        if (schemes.cDivergence.Count > 0)
                        {
                            var row = (VDivergenceScheme) DivergenceGrid.GetFocusedRow();
                            schemes.cDivergence.Remove(row);
                        }
                    }
                    break;
                case "Gradient":
                    {
                        if (schemes.cGradient.Count > 0)
                        {
                            var row = (VGradientScheme) GradientGrid.GetFocusedRow();
                            schemes.cGradient.Remove(row);
                        }
                    }
                    break;
                case "Laplacian":
                    {
                        if (schemes.cLaplacian.Count > 0)
                        {
                            var row = (VLaplacianScheme) LaplacianGrid.GetFocusedRow();
                            schemes.cLaplacian.Remove(row);
                        }
                    }
                    break;
                case "TimeScheme":
                    {
                        if (schemes.cTime.Count > 0)
                        {
                            var row = (VTimeScheme) TimeGrid.GetFocusedRow();
                            schemes.cTime.Remove(row);
                        }
                    }
                    break;
                default:
                    {
                        if (schemes.cFlux.Count > 0)
                        {
                            var row = (VFluxControl) FluxGrid.GetFocusedRow();
                            schemes.cFlux.Remove(row);
                        }
                    }
                    break;
            }
        }
    }
}
