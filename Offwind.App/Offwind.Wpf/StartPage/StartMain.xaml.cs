using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;

namespace Offwind.StartPage
{
    /// <summary>
    /// Interaction logic for StartMain.xaml
    /// </summary>
    public partial class StartMain : UserControl
    {
        public StartMain()
        {
            InitializeComponent();
        }

        private Brush linkBg = new SolidColorBrush(Color.FromRgb(90, 154, 135));
        private void textBlock3_MouseEnter(object sender, MouseEventArgs e)
        {
            textBlock3.Background = linkBg;
        }
    }
}
