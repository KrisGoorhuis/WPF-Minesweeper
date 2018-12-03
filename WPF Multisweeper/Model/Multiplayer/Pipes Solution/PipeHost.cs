using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Multisweeper
{
    class PipeHost
    {
        public static NamedPipeServerStream serverStream;

        StreamReader serverReader;
        StreamWriter serverWriter;

        string defaultPipeName;

        Square[,] test = MainWindow.playField;
        
        public PipeHost(string name)
        {
            defaultPipeName = name;

            serverStream = new NamedPipeServerStream(defaultPipeName);

            serverReader = new StreamReader(serverStream);
            serverWriter = new StreamWriter(serverStream);
        }

       



        public void SendBoardToClients()
        {
            Square[,] hostPlayField = MainWindow.playField;

            BinaryFormatter f = new BinaryFormatter();

            f.Serialize(serverStream, MainWindow.playField);
        }

        public void SendClickToClients()
        {

        }

        //public void SendData(data)
        //{
        //    Square[,] hostPlayField = MainWindow.playField;

        //    BinaryFormatter f = new BinaryFormatter();

        //    f.Serialize(serverStream, MainWindow.playField);
        //}

        public void StartServer()
        {

            serverStream.WaitForConnection();
            Task.Run(() =>
            {

                ListenForMessages();
                //StreamReader serverReader = new StreamReader(server);
                //StreamWriter serverWriter = new StreamWriter(server);
                //while (true)
                //{
                //    string line = serverReader.ReadLine();
                //    serverWriter.WriteLine(String.Join("", line.Reverse()));
                //    serverWriter.Flush();
                //}
            });
        }



        public void ListenForMessages()
        {

            Task.Run(() =>
            {


                string message;

                while (true)
                {
                    message = serverReader.ReadLine();

                    Console.WriteLine(message);

                    if (message == "connect")
                    {
                        serverWriter.WriteLine("success");
                        SendBoardToClients();
                    }
                    else
                    {
                        serverWriter.WriteLine("Wasn't quite right");
                    }
                    serverWriter.Flush();

                }
            });
        }
    }
}
