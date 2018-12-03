using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// Interaction logic for HostMultiplayer.xaml
    /// </summary>
    public partial class HostMultiplayer : Window
    {
        GameHost gameHost;
        string defaultName = "MinePipe";



        public HostMultiplayer()
        {
            InitializeComponent();
        }

        void BeginHosting(object sender, RoutedEventArgs e)
        {
            TextBox textBox;
            PipeHost pipeHost = new PipeHost(defaultName);

            pipeHost.StartServer();

            HostButton.Background = Brushes.DarkSlateGray;
            HostButton.Content = "Hosting";
            HostButton.IsHitTestVisible = false;

            ConnectionStatus.Children.RemoveAt(0);
            textBox = new TextBox();
            textBox.Text = "Server started.";
            ConnectionStatus.Children.Add(textBox);

            textBox = new TextBox();
            textBox.Text = "Awaiting client...";
            ConnectionStatus.Children.Add(textBox);

            Task.Run( () =>
            {
                //pipeHost.StartServer();
                //pipeHost.SendBoardToClient();
                pipeHost.ListenForMessages();

                // Dispatcher.Invoke thing below fixes the error
                // System.InvalidOperationException: 'The calling thread must be STA, because many UI components require this.'
                // somehow. I don't know what this is about.
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    textBox = new TextBox();
                    textBox.Text = "Client connected!";
                    ConnectionStatus.Children.Add(textBox);

                });

                pipeHost.ListenForMessages();
            });

        }


        void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

    }

}
