using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace EchoClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            EchoClient client = new EchoClient("127.0.0.1",12000);
            client.Run();
        }
    }


    class EchoClient
    {
        private string servername;
        private int port;

        public EchoClient(string servername, int port)
        {
            this.servername = servername;
            this.port = port;
        }

        public void Run()
		{
            System.Console.WriteLine("Echo client startet p� "+servername +" port:"+port);
            System.Console.WriteLine("Afslut input med BYE");


            //// Instanti�r socket - forbinder socket til server
            TcpClient server = new TcpClient(servername, port);

            NetworkStream stream = server.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            string serverData = reader.ReadLine(); // L�ser velkomst fra server
            Console.WriteLine(serverData); // Skriver velkomst til sk�rmen

            string userInput;
            userInput = Console.ReadLine();  // f�rste brugerinput

            while (!userInput.Trim().ToUpper().Equals("BYE"))
            {
                writer.WriteLine(userInput); // Skriver tekst til serveren
                writer.Flush(); // T�mmer tcp-bufferen
                serverData = reader.ReadLine(); // L�ser ekko fra serveren
                Console.WriteLine(serverData); // Skriver ekko p� sk�rmen

                userInput = Console.ReadLine();  // nyt brugerinput
            }


            writer.WriteLine("BYE"); // send shut down kommando til serveren
            writer.Flush(); // T�mmer tcp-bufferen
            Console.WriteLine("Forbindelsen til serveren lukkes ned ...\n");
            writer.Close();
            reader.Close();
            stream.Close();
            server.Close();
		}
    }
}
