using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Multisweeper
{
    /// <summary>
    /// Interaction logic for CustomSinglePlayer.xaml
    /// </summary>
    public partial class CustomSinglePlayer : Window
    {
        public CustomSinglePlayer()
        {
            InitializeComponent();
        }


        void StartCustomSinglePlayer(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = this.Owner as MainWindow;

            int width = Convert.ToInt32(customWidthField.Text);
            int height = Convert.ToInt32(customHeightField.Text);
            int saturation = Convert.ToInt32(customSaturationField.Text);

            mainWindow.StartCustomSinglePlayer(width, height, saturation);

            this.Close();
        }
    }
}
