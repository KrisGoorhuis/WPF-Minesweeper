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
    /// Interaction logic for JoinMultiplayerSetup.xaml
    /// </summary>
    public partial class JoinMultiplayerSetup : Window
    {
        public JoinMultiplayerSetup()
        {
            InitializeComponent();
        }


        void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void ConnectToGame(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = this.Owner as MainWindow;
            


            mainWindow.JoinMultiplayerGame();

            this.Close();
        }
    }
}
