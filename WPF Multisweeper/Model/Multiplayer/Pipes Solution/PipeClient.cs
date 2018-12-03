using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Multisweeper
{
    public class PipeClient
    {
        public static NamedPipeClientStream clientStream;
        StreamReader clientReader;
        StreamWriter clientWriter;

        string defaultPipeName;



        public PipeClient(string name)
        {
            defaultPipeName = name;
        }

        public void ConnectToPipe()
        {
            clientStream = new NamedPipeClientStream(defaultPipeName);
            clientStream.Connect();

            clientReader = new StreamReader(clientStream);
            clientWriter = new StreamWriter(clientStream);

            clientWriter.WriteLine("connect");
            clientWriter.Flush();

            BinaryFormatter f = new BinaryFormatter();
            Square[,] newPlayField = (Square[,])f.Deserialize(clientStream);

            MainWindow.playField = newPlayField;

        }

        void ExchangeBoard()
        {
            Square[,] hostPlayField = MainWindow.playField;

            BinaryFormatter f = new BinaryFormatter();

            f.Serialize(clientStream, MainWindow.playField);
        }

        public static void SendClickToHost(Point clickCoords)
        {

        }

        public void DisconnectFromPipe()
        {
            clientWriter.WriteLine("close");
            clientWriter.Flush();

            clientStream.Close();
        }

    }
}
