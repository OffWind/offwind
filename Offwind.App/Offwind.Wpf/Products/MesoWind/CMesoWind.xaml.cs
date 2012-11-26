using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.Xpf.Ribbon.Customization;
using Microsoft.Maps.MapControl.WPF;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;
using Point = System.Windows.Point;

namespace Offwind.Products.MesoWind
{
    /// <summary>
    /// Interaction logic for CMesoWind.xaml
    /// </summary>
    public partial class CMesoWind : UserControl, IProjectItemView, IDisposable
    {
        private const string DbDir = @"D:\projects\offwind.from-carlos\offshore.wind.database\offshore.tab\";
        private readonly VDataImport _model = new VDataImport();
        private VMesoWind _projectModel;
        private bool _isFiltered;
        private Pushpin _pushpin = new Pushpin();
        private Pushpin _pushpinGrid = new Pushpin();

        public CMesoWind()
        {
            InitializeComponent();
            DataContext = _model;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Load Database
            foreach (var d in Directory.EnumerateFiles(DbDir, "*.dat.tab", SearchOption.TopDirectoryOnly))
            {
                var f = System.IO.Path.GetFileName(d);
                f = f.Replace(".dat.tab", "");
                var parts = f.Split('_');

                var longitude = ParseDecimal(parts[0].TrimEnd("NESW".ToCharArray()));
                var latitude = ParseDecimal(parts[1].TrimEnd("NESW".ToCharArray()));
                if (parts[0].EndsWith("W")) longitude = -longitude;
                if (parts[1].EndsWith("S")) latitude = -latitude;

                var dbItem = new DatabaseItem();
                dbItem.Longitude = longitude;
                dbItem.Latitude = latitude;
                dbItem.FileName = System.IO.Path.GetFileName(d);
                _model.DatabaseItems.Add(dbItem);
            }

            BindDatabase();
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
            _projectModel = (VMesoWind) vProject.ProjectModel;
        }

        private void gridDatabase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dbItem = (DatabaseItem)gridDatabase.View.FocusedRow;
            if (dbItem == null) return;

            _pushpinGrid.Location = new Location((double)dbItem.Latitude, (double)dbItem.Longitude);

            if (!Markers.Children.Contains(_pushpinGrid))
            {
                _pushpinGrid.Background = new SolidColorBrush(Colors.RoyalBlue);
                Markers.Children.Add(_pushpinGrid);
            }
        }

        private void gridDatabase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DoImport();
        }

        private void DoImport()
        {
            _model.FreqByDirs.Clear();
            _model.FreqByBins.Clear();
            try
            {
                Import();
                Calculate();
                PushUpdatesIntoProject();
            }
            catch (Exception ex)
            {
                _model.NBins = 0;
                _model.NDirs = 0;
                _model.FreqByDirs.Clear();
                _model.FreqByBins.Clear();
                MessageBox.Show(ex.Message);
            }

            Debug.Assert(_model.FreqByDirs.Count == _model.NDirs);
            Debug.Assert(_model.FreqByBins.Count == _model.NBins);
        }

        private void Import()
        {
            var dbItem = (DatabaseItem) gridDatabase.View.FocusedRow;
            var path = System.IO.Path.Combine(DbDir, dbItem.FileName);
            using (var f = new StreamReader(path))
            {
                var lineN = 0;
                while (!f.EndOfStream)
                {
                    var line = f.ReadLine();
                    lineN++;
                    Trace.WriteLine(line);
                    switch (lineN)
                    {
                        case 1:
                            break;
                        case 2:
                            var line2 = line.Trim().Split("\t ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            _model.NBins = ParseInt(line2[2]);
                            break;
                        case 3:
                            var line3 = line.Trim().Split("\t ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            _model.NDirs = ParseInt(line3[0]);
                            break;
                        case 4:
                            var line4 = line.Trim().Split("\t ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            foreach (var s in line4)
                            {
                                _model.FreqByDirs.Add(ParseDecimal(s));
                            }
                            break;
                        default:
                            var line5N = line.Trim().Split("\t ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            Debug.Assert(line5N.Length == _model.NDirs + 1); // 1st cell contains bin number
                            var tmp = new decimal[_model.NDirs];
                            for (var i = 0; i < _model.NDirs; i++)
                            {
                                tmp[i] = ParseDecimal(line5N[i + 1]);
                            }
                            _model.FreqByBins.Add(tmp);
                            break;
                    }
                }
            }
        }

        private void Calculate()
        {

            // MeanVelocityPerDir
            _model.MeanVelocityPerDir.Clear();
            _model.MeanVelocityPerDir.AddRange(new decimal[_model.NDirs]);
            for (var binIdx = 0; binIdx < _model.NBins; binIdx++)
                for (var dirIdx = 0; dirIdx < _model.NDirs; dirIdx++)
                {
                    var velocity = binIdx + 1;
                    _model.MeanVelocityPerDir[dirIdx] += (decimal)(velocity * (double)_model.FreqByBins[binIdx][dirIdx] / 1000);
                }

            // Velocity frequencies
            _model.VelocityFreq.Clear();
            for (int binIdx = 0; binIdx < _model.NBins; binIdx++)
            {
                decimal freq = 0;
                for (int dirIdx = 0; dirIdx < _model.NDirs; dirIdx++)
                {
                    freq += _model.FreqByBins[binIdx][dirIdx] / 1000 * _model.FreqByDirs[dirIdx];
                }
                _model.VelocityFreq.Add(new HPoint(binIdx, freq));
            }
        }

        private void PushUpdatesIntoProject()
        {
            _projectModel.NBins = _model.NBins;
            _projectModel.NDirs = _model.NDirs;
            _projectModel.Latitude = _model.FilterLatitude;
            _projectModel.Longitude = _model.FilterLongitude;

            _projectModel.FreqByBins.Clear();
            _projectModel.FreqByBins.AddRange(_model.FreqByBins);

            _projectModel.FreqByDirs.Clear();
            _projectModel.FreqByDirs.AddRange(_model.FreqByDirs);

            _projectModel.VelocityFreq.Clear();
            _projectModel.VelocityFreq.AddRange(_model.VelocityFreq);

            _projectModel.MeanVelocityPerDir.Clear();
            _projectModel.MeanVelocityPerDir.AddRange(_model.MeanVelocityPerDir);

            _projectModel.NotifyTargets(ProductTargets.WindRose);
            _projectModel.NotifyTargets(ProductTargets.Histogram);
        }

        private void BindDatabase()
        {
            var items = new List<DatabaseItem>();
            if (!_isFiltered)
            {
                items.AddRange(_model.DatabaseItems);
                colDistance.Visible = false;
            }
            else
            {
                foreach (var item in _model.DatabaseItems)
                {
                    var sCoord = new GeoCoordinate((double)item.Latitude, (double)item.Longitude);
                    var eCoord = new GeoCoordinate((double)_model.FilterLatitude, (double)_model.FilterLongitude);

                    item.Distance = sCoord.GetDistanceTo(eCoord); //meters
                    if (item.Distance > 100000) continue;
                    items.Add(item);
                }
                items.Sort((a,b) => a.Distance.CompareTo(b.Distance));
                colDistance.Visible = true;
            }
            gridDatabase.ItemsSource = null;
            gridDatabase.ItemsSource = items;
        }

        private int ParseInt(string input)
        {
            int ir;
            if (int.TryParse(input, out ir))
                return ir;
            decimal dr;
            if (decimal.TryParse(input, out dr))
                return Convert.ToInt32(dr);
            return 0;
        }

        private decimal ParseDecimal(string input)
        {
            decimal dr;
            if (decimal.TryParse(input, out dr))
                return dr;
            return 0;
        }

        private void MainMap_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(this.MainMap);
            Location location = new Location();
            MainMap.TryViewportPointToLocation(p, out location);
            _model.CurrentLongitude = (decimal)location.Longitude;
            _model.CurrentLatitude = (decimal)location.Latitude;
        }

        private void MainMap_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            Point p = e.GetPosition(this.MainMap);
            Location location = new Location();
            MainMap.TryViewportPointToLocation(p, out location);

            _pushpin.Location = new Location(location.Latitude, location.Longitude);

            if (!_isFiltered)
            {
                Markers.Children.Add(_pushpin);
            }

            _model.FilterLongitude = (decimal)location.Longitude;
            _model.FilterLatitude = (decimal)location.Latitude;
            _isFiltered = true;
            BindDatabase();
        }

        private void MainMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        private void LGroupData_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Debug.WriteLine("LGroupMap.Width: {0}", LGroupMap.ActualWidth);
            Debug.WriteLine("LGroupData.Width: {0}", LGroupData.ActualWidth);
        }

        private void bbMesoResetFilter_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            _isFiltered = false;
            Markers.Children.Remove(_pushpin);
            BindDatabase();
        }

        private void bbMesoImport_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            DoImport();
        }

        public void Dispose()
        {
            gridMapContainer.Children.Remove(MainMap);
            MainMap.Dispose();
        }
    }
}
