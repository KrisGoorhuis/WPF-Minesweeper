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

            int width;
            int height;
            int mineCount;

            try
            {
                width = Convert.ToInt32(customWidthField.Text);
                height = Convert.ToInt32(customHeightField.Text);
                mineCount = Convert.ToInt32(customMineCountField.Text);

                if (width > 0 && height > 0 && mineCount > 0)
                {
                    mainWindow.StartCustomGame(width, height, mineCount);
                    this.Close();
                }
                else
                {
                    customGameError.Text = "Fields must be > 0";
                }
            }
            catch
            {
                customGameError.Text = "Fields must be numbers";
            }
        }

        void CloseWindow()
        {
            this.Close();
        }
    }
}
