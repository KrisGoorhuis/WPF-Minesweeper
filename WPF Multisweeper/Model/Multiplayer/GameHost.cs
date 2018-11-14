using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Windows;

namespace Multisweeper
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GameHost" in both code and config file together.
    public class GameHost : IGameHost
    {
        ServiceHost gameHost;

        public GameHost()
        {
            try
            {
                // typeof(HostService) ?
                // What's best practice?
                gameHost = new ServiceHost(typeof(GameService), new Uri("http://localhost:8080/"));

                // One endpoint per address is added if no endpoints are specified. We have an endpoint by this line.
                //ServiceEndpoint gameEndpoint = gameHost.AddServiceEndpoint(typeof(IGameService), new NetTcpBinding(), "net.tcp://localhost:8000/GameService");

                // .Faulted is an event. This is essentially a catch, right?
                gameHost.Faulted += GameHost_Faulted;

                gameHost.Open();
                Debug.WriteLine("test line!");
                Console.WriteLine("The Product service is running and is listening on:");

                ////Console.WriteLine("{0} ({1})", gameHost.Address.ToString(), gameHost.Binding.Name);
                MessageBoxResult box = MessageBox.Show("Should be listening now",
                                          "Success",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
            
            }

            finally
            {
                if (gameHost.State == CommunicationState.Faulted)

                {
                    gameHost.Abort();
                }

                else

                {
                    //gameHost.Close();
                }
            }
        }
        public void DoWork()
        {

        }

        static void GameHost_Faulted(object sender, System.EventArgs e)
        {
            MessageBoxResult box = MessageBox.Show("Faulted somehow",
                                          "Faulted",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
        }
    }
}
