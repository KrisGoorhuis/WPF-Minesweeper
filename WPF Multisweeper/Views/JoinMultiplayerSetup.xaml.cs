using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.ServiceModel;
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
        string serverIp = "localhost";
        int port = 8080;
        MainWindow mainWindow;
        string defaultName = "MinePipe";



        public JoinMultiplayerSetup()
        {
            InitializeComponent();

            MainWindow mainWindow = this.Owner as MainWindow;

        }


        void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void JoinGame(object sender, RoutedEventArgs e)
        {

            PipeClient pipeClient = new PipeClient(defaultName);

            pipeClient.ConnectToPipe();
            
           


            //GameClient client = new GameClient();
            //mainWindow.JoinMultiplayerGame();
        }

        void ConnectToGame(object sender, RoutedEventArgs e)
        {
            

            

            //string placeholder = "placeholder";

            //TcpClient client = new TcpClient(serverIp, port);

            //int byteCount = Encoding.ASCII.GetByteCount(placeholder);

            //byte[] sendData = new byte[byteCount];

            //sendData = Encoding.ASCII.GetBytes(placeholder);

            //NetworkStream stream = client.GetStream();

            //stream.Write(sendData, 0, sendData.Length);

            //stream.Close();
            //client.Close();


            //mainWindow.JoinMultiplayerGame();

            //this.Close();
        }
    }
}
