using System;
using System.Drawing;
using System.Windows.Forms;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Labels;
using devDept.Geometry;
using Point = System.Drawing.Point;
using ToolBarButton = devDept.Eyeshot.ToolBarButton;

namespace Offwind.Products.Sowfa.UI.AblGeometry
{
    public sealed class Renderer
    {
        private ViewportLayout _viewportLayout;
        private double _width;
        private double _height;
        private double _length;

        public void Set(decimal width, decimal length, decimal height)
        {
            _width = Convert.ToDouble(width);
            _height = Convert.ToDouble(height);
            _length = Convert.ToDouble(length);
        }

        public ViewportLayout InitViewport()
        {
            var displayModeSettingsRendered1 = new DisplayModeSettingsRendered(true, edgeColorMethodType.EntityColor, Color.Black, 1F, 2F, silhouettesDrawingType.LastFrame, false, shadowType.Realistic, null, false, true, 0.3F, realisticShadowQualityType.High);
            var backgroundSettings1 = new BackgroundSettings(backgroundStyleType.LinearGradient, Color.WhiteSmoke, Color.White, Color.FromArgb(102, 163, 210), 0.75D, null);
            var camera1 = new Camera(new Point3D(0D, 0D, 0D), 100D, new Quaternion(0.12940952255126034D, 0.22414386804201339D, 0.4829629131445341D, 0.83651630373780794D), projectionType.Perspective, 50D, 8D);
            var toolBarButton1 = new ToolBarButton(null, "Zoom Window", "Zoom Window", ToolBarButtonStyle.ToggleButton, true);
            var toolBarButton2 = new ToolBarButton(null, "Zoom", "Zoom", ToolBarButtonStyle.ToggleButton, true);
            var toolBarButton3 = new ToolBarButton(null, "Pan", "Pan", ToolBarButtonStyle.ToggleButton, true);
            var toolBarButton4 = new ToolBarButton(null, "Rotate", "Rotate", ToolBarButtonStyle.ToggleButton, true);
            var toolBarButton5 = new ToolBarButton(null, "Zoom Fit", "Zoom Fit", ToolBarButtonStyle.PushButton, true);
            var toolBar1 = new devDept.Eyeshot.ToolBar(toolBarPositionType.HorizontalTopRight, 32, 3, 4, Color.FromArgb(255, 146, 0), true, new[] {
                toolBarButton1,
                toolBarButton2,
                toolBarButton3,
                toolBarButton4,
                toolBarButton5
            });
            var legend1 = new Legend(0D, 100D, "Title", "Subtitle", new Point(24, 24), new Size(10, 30), true, false, false, "{0:0.##}", System.Drawing.Color.Transparent, System.Drawing.Color.Black, System.Drawing.Color.Black, new System.Drawing.Font("Tahoma", 10F, FontStyle.Bold), new Font("Tahoma", 8.25F), new[] {
                Color.FromArgb(0, 0, 255),
                Color.FromArgb(0, 63,  255),
                Color.FromArgb(0, 127, 255),
                Color.FromArgb(0, 191, 255),
                Color.FromArgb(0, 255, 255),
                Color.FromArgb(0, 255, 191),
                Color.FromArgb(0, 255, 127),
                Color.FromArgb(0, 255, 63),
                Color.FromArgb(0, 255, 0),
                Color.FromArgb(63,  255, 0),
                Color.FromArgb(127, 255, 0),
                Color.FromArgb(191, 255, 0),
                Color.FromArgb(255, 255, 0),
                Color.FromArgb(255, 191, 0),
                Color.FromArgb(255, 127, 0),
                Color.FromArgb(255, 63,  0),
                Color.FromArgb(255, 0,   0)
            });

            var originSymbol1 = new OriginSymbol(2, originSymbolStyleType.CoordinateSystem, new Font("Tahoma", 8.25F), Color.Black, Color.Red, Color.Green, Color.Blue, "Origin", "X", "Y", "Z", true);
            var rotateSettings1 = new RotateSettings(new MouseButton(MouseButtons.Middle, ModifierKeys.None), 10D, true, 1D, rotationStyleType.Trackball, rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0D, 0D, 0D));
            var zoomSettings1 = new ZoomSettings(new MouseButton(MouseButtons.Middle, ModifierKeys.Shift), 25, true, zoomStyleType.AtCursorLocation, false, 1D, Color.DeepSkyBlue, perspectiveFitType.Accurate);
            var viewport1 = new Viewport(new Point(0, 0),
                new Size(100, 10000),
                backgroundSettings1,
                camera1,
                toolBar1,
                new[] {
                    legend1
                },
                displayType.Rendered, true, false, false, CreateGrid(10),
                originSymbol1, false, rotateSettings1, zoomSettings1,
                new PanSettings(new MouseButton(MouseButtons.Middle, ModifierKeys.Ctrl), 25, true));

            _viewportLayout = new SingleViewportLayout();
            _viewportLayout.Dock = DockStyle.Fill;
            _viewportLayout.Cursor = Cursors.Arrow;
            _viewportLayout.Name = "SingleViewPort";
            _viewportLayout.Rendered = displayModeSettingsRendered1;
            _viewportLayout.Size = new Size(431, 405);
            _viewportLayout.TabIndex = 0;
            _viewportLayout.Viewports.Add(viewport1);

            return _viewportLayout;
        }

        public void Render()
        {
            _viewportLayout.Clear();

            SetBackgroundStyleAndColor();

            // Reset grid

            double avg = (double)(_width + _length + _height) / 3;
            double fontHeight = avg / 14;
            double gridExtension = avg / 5;
            _viewportLayout.Grid = CreateGrid(gridExtension);
            int box = _viewportLayout.Layers.Add("Box", Color.DarkGray);
            var bottom = new[] {
                new Point3D(0, 0, 0),
                new Point3D(_width, 0, 0),
                new Point3D(_width, _length, 0),
                new Point3D(0, _length, 0)
            };
            var top = new[] {
                new Point3D(0, 0, _height),
                new Point3D(_width, 0, _height),
                new Point3D(_width, _length, _height),
                new Point3D(0, _length, _height)
            };
            var south = new[] {
                bottom[0], bottom[1], top[1], top[0]
            };
            var north = new[] {
                bottom[2], bottom[3], top[3], top[2]
            };
            var west = new[] {
                bottom[0], bottom[3], top[3], top[0]
            };
            var east = new[] {
                bottom[1], bottom[2], top[2], top[1]
            };
            _viewportLayout.Entities.Add(new Quad(bottom[0], bottom[1], bottom[2], bottom[3]), box, Color.Cyan);
            _viewportLayout.Entities.Add(new Quad(top[0], top[1], top[2], top[3]), box, Color.BurlyWood);
            _viewportLayout.Entities.Add(new Quad(south[0], south[1], south[2], south[3]), box, Color.Aquamarine);
            _viewportLayout.Entities.Add(new Quad(north[0], north[1], north[2], north[3]), box, Color.Chartreuse);
            _viewportLayout.Entities.Add(new Quad(west[0], west[1], west[2], west[3]), box, Color.Beige);
            _viewportLayout.Entities.Add(new Quad(east[0], east[1], east[2], east[3]), box, Color.Cornsilk);

            // Labels
            var halfHeight = _height / 2;
            _viewportLayout.Labels.Add(new LeaderAndText((south[0].X + south[1].X) / 2, south[0].Y, halfHeight, "South", new Font("Tahoma", 8.25f), Color.Black, new Vector2D(10, 10)));
            _viewportLayout.Labels.Add(new LeaderAndText((north[0].X + north[1].X) / 2, north[0].Y, halfHeight, "North", new Font("Tahoma", 8.25f), Color.Black, new Vector2D(10, 10)));
            _viewportLayout.Labels.Add(new LeaderAndText(east[0].X, (east[0].Y + east[1].Y) / 2, halfHeight, "East", new Font("Tahoma", 8.25f), Color.Black, new Vector2D(10, 10)));
            _viewportLayout.Labels.Add(new LeaderAndText(west[0].X, (west[0].Y + west[1].Y) / 2, halfHeight, "West", new Font("Tahoma", 8.25f), Color.Black, new Vector2D(10, 10)));

            _viewportLayout.Labels.Add(new LeaderAndText((top[0].X + top[2].X) / 2, (top[0].Y + top[2].Y) / 2, _height, "Top", new Font("Tahoma", 8.25f), Color.Black, new Vector2D(10, 10)));
            _viewportLayout.Labels.Add(new LeaderAndText((bottom[0].X + bottom[2].X) / 2, (bottom[0].Y + bottom[2].Y) / 2, 0, "Bottom", new Font("Tahoma", 8.25f), Color.Black, new Vector2D(10, 10)));

            // Dimensions
            //xyPlane.Rotate(Math.PI / 2, Vector3D.AxisZ, Point3D.Origin);
            //Plane dimPlane2 = Plane.YZ;
            //dimPlane2.Rotate(Math.PI / 2, Vector3D.AxisX, Point3D.Origin);

            Plane xyPlane = Plane.XY;
            var widthDim = new LinearDim(xyPlane, bottom[0], bottom[1], new Point3D(0, -gridExtension * 2, gridExtension * 2), fontHeight);
            widthDim.TextSuffix = " m";
            _viewportLayout.Entities.Add(widthDim, box, Color.Black);

            xyPlane.Rotate(Math.PI / 2, Vector3D.AxisZ, Point3D.Origin);
            var lengthDim = new LinearDim(xyPlane, bottom[1], bottom[2], new Point3D(bottom[1].X + gridExtension * 2, gridExtension * 2, gridExtension * 2), fontHeight);
            lengthDim.TextSuffix = " m";
            _viewportLayout.Entities.Add(lengthDim, box, Color.Black);

            Plane zxPlane = Plane.ZX;
            zxPlane.Rotate(Math.PI, Vector3D.AxisZ, Point3D.Origin);
            var heightDim = new LinearDim(zxPlane, bottom[1], top[1], new Point3D(bottom[1].X, gridExtension * 2, gridExtension * 2), fontHeight);
            heightDim.TextSuffix = " m";
            _viewportLayout.Entities.Add(heightDim, box, Color.Black);

            _viewportLayout.ZoomFit();
            //viewportLayout.WriteAsciiSTL("c:\\temp\\out.txt", false);
        }

        private Grid CreateGrid(double extension)
        {
            return new Grid(
                new Point3D(-extension, -extension, 0D),
                new Point3D(_width + extension, _length + extension, 0D),
                10D,
                new Plane(new Point3D(0D, 0D, 0D), new Vector3D(0D, 0D, 1D)),
                Color.FromArgb(127, 128, 128, 128),
                Color.FromArgb(127, 32, 32, 32),
                false, true, false, false, 10, 100, 10,
                Color.FromArgb(127, 90, 90, 90));
        }

        private void SetBackgroundStyleAndColor()
        {
            switch (_viewportLayout.DisplayMode)
            {
                case displayType.HiddenLines:
                    _viewportLayout.Background.Style = backgroundStyleType.Solid;
                    _viewportLayout.Background.TopColor = Color.FromArgb(0xD2, 0xD0, 0xB9);
                    break;
                default:
                    _viewportLayout.Background.Style = backgroundStyleType.LinearGradient;
                    _viewportLayout.Background.BottomColor = Color.WhiteSmoke;
                    _viewportLayout.Background.TopColor = Color.FromArgb(0x66, 0xa3, 0xd2);
                    _viewportLayout.CompileUserInterfaceElements();
                    break;
            }
        }
    }
}
