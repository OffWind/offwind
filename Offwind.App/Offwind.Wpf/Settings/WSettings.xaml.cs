using System.Windows;

namespace Offwind.Settings
{
    /// <summary>
    /// Interaction logic for WSettings.xaml
    /// </summary>
    public partial class WSettings : Window
    {
        public WSettings()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new NonRoamingUserSettings().Destroy();
        }
    }
}
