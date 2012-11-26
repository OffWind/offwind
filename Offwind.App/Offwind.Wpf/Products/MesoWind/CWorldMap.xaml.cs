using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Maps.MapControl.WPF;
using Offwind.Infrastructure;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace Offwind.Products.MesoWind
{
    /// <summary>
    /// Interaction logic for CWorldMap.xaml
    /// </summary>
    public partial class CWorldMap : UserControl, IProjectItemView
    {
        private VMesoWind _projectModel;
        Point start;
        Point origin;
        
        public CWorldMap()
        {
            InitializeComponent();
        }

        private void EmbeddedMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (image.IsMouseCaptured)
            {
                Debug.WriteLine("Capture Move");
                var tt = (TranslateTransform) ((TransformGroup) image.RenderTransform)
                                                  .Children.First(tr => tr is TranslateTransform);
                Vector v = start - e.GetPosition(border);
                tt.X = origin.X - v.X;
                tt.Y = origin.Y - v.Y;
            }
            else
            {
                Debug.WriteLine("Normal Move");
                Point p = e.GetPosition(image);
                double scale = 360/image.ActualWidth;
                Point pGeo = new Point(p.X*scale - 180, 90 - p.Y*scale);
                locationLong.Text = string.Format("{0}", pGeo.X);
                locationLat.Text = string.Format("{0}", pGeo.Y);

                //Transform to UTM
                CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
                ICoordinateSystem wgs84geo = ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84;
                int zone = (int) Math.Ceiling((pGeo.X + 180)/6);
                //ICoordinateSystem utm = ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WGS84_UTM(zone, pGeo.Y > 0);
                //ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(wgs84geo, utm);
                //Point pUtm = trans.MathTransform.Transform(pGeo);
                //locationX.Text = string.Format("N{0}", pUtm.Y);
                //locationY.Text = string.Format("E{0}", pUtm.X);
                locationZone.Text = string.Format("Zone {0}{1}", zone, pGeo.Y > 0 ? 'N' : 'S');
            }
        }

        private void EmbeddedMap_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Debug.WriteLine("Wheel");
            var st = (ScaleTransform)((TransformGroup)image.RenderTransform).Children.First(tr => tr is ScaleTransform);
            double zoom = e.Delta > 0 ? .2 : -.2;
            st.ScaleX += zoom;
            st.ScaleY += zoom;
        }

        private void EmbeddedMap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Point p = e.GetPosition(image);
                double scale = 360 / image.ActualWidth;
                Point pGeo = new Point(p.X * scale - 180, 90 - p.Y * scale);
                PushUpdatesIntoProjectModel(pGeo.X, pGeo.Y);
            }
            else
            {
                Debug.WriteLine("LeftButtonDown: Capture ON");
                image.CaptureMouse();
                var tt = (TranslateTransform)((TransformGroup)image.RenderTransform).Children.First(tr => tr is TranslateTransform);
                start = e.GetPosition(border);
                origin = new Point(tt.X, tt.Y);
            }
        }

        private void EmbeddedMap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("LeftButtonUp: Capture OFF");
            image.ReleaseMouseCapture(); 
        }

        private void MainMap_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(this.MainMap);
            Location location = new Location();
            MainMap.TryViewportPointToLocation(p, out location);
            locationLong.Text = string.Format("{0}", location.Longitude);
            locationLat.Text = string.Format("{0}", location.Latitude);
        }

        private void MainMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this.MainMap);
            Location location = new Location();
            MainMap.TryViewportPointToLocation(p, out location);
            locationLong.Text = string.Format("{0}", location.Longitude);
            locationLat.Text = string.Format("{0}", location.Latitude);
            PushUpdatesIntoProjectModel(location.Longitude, location.Latitude);
        }

        private void PushUpdatesIntoProjectModel(double longitude, double latitude)
        {
            _projectModel.Longitude = (decimal)longitude;
            _projectModel.Latitude = (decimal)latitude;
            _projectModel.NotifyTargets(ProductTargets.DataImporter);
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
            MainMap.Center.Longitude = (double)_projectModel.Longitude;
            MainMap.Center.Latitude = (double)_projectModel.Latitude;
        }
    }
}
