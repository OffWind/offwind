using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Offwind.StartPage
{
    /// <summary>
    /// Interaction logic for StartDialog.xaml
    /// </summary>
    public partial class StartDialog : Window
    {
        public StartDialog()
        {
            InitializeComponent();
        }

        private void borderEngineering_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void borderEngineering_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        private void ButtonMouseEnter(object sender, MouseEventArgs e)
        {
            var b = (Button) sender;
            if (b.Name == "buttonCFD")
            {
                descriptionStart.Visibility = Visibility.Hidden;
                descriptionCFD.Visibility = Visibility.Visible;
                descriptionEngineering.Visibility = Visibility.Hidden;
            }
            else if (b.Name== "buttonEngineering")
            {
                descriptionStart.Visibility = Visibility.Hidden;
                descriptionCFD.Visibility = Visibility.Hidden;
                descriptionEngineering.Visibility = Visibility.Visible;
            }
        }

        private void ButtonMouseLeave(object sender, MouseEventArgs e)
        {
            descriptionStart.Visibility = Visibility.Visible;
            descriptionCFD.Visibility = Visibility.Hidden;
            descriptionEngineering.Visibility = Visibility.Hidden;
        }
    }
}
