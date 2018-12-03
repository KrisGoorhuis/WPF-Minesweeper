using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Multisweeper
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GameHost" in both code and config file together.
    public class GameHost
    {
        public ServiceHost host;


        //Socket version
        //IPAddress ip;
        //TcpListener server;
        //TcpClient client;

        public GameHost()
        {


            // Socket version
            //ip = Dns.GetHostEntry("localhost").AddressList[0];
            //server = new TcpListener(ip, 8080);
            //client = default(TcpClient);

            //StartServer();
        }


        public void StartHostService()
        {
            host = new ServiceHost(typeof(HostService));

            host.Open();
         

        }





        // Socket version
        //public async void StartServer()
        //{
        //    try
        //    {
        //        server.Start();
        //        Console.WriteLine("Server started on {0}:8080", ip);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }

        //    //await AsyncListenForClients();

        //    //Console.WriteLine("Client connected");
        //}

        //public async Task AsyncListenForClients()
        //{

        //    await Task.Run(() =>
        //    {
        //        client = server.AcceptTcpClient();
        //    });

        //    byte[] receivedBuffer = new byte[100];
        //    NetworkStream stream = client.GetStream();

        //    stream.Read(receivedBuffer, 0, receivedBuffer.Length);

        //    string message = Encoding.ASCII.GetString(receivedBuffer, 0, receivedBuffer.Length);

        //    Console.WriteLine(message);

        //    return;
        //}

       
    }
}
