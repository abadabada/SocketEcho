using System;
using System.IO;
using System.Net;
using System.Net.Sockets;


namespace EchoServerApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			EchoServer server = new EchoServer(12000);
            server.Run();
		}
	}

	class EchoServer
	{
        private IPAddress ip = IPAddress.Parse("127.0.0.1");
        private int port;
        private volatile bool stop = false;

        public EchoServer(int port)
        {
            this.ip = IPAddress.Parse("127.0.0.1");
            this.port = port;
        }

		public void Run()
		{
			System.Console.WriteLine("Echo server startet på port:"+port);
			
			TcpListener listener =	new TcpListener(ip, port);
			listener.Start();
			while (!stop)
			{
				System.Console.WriteLine("Echo server klar");

				Socket clientSocket = listener.AcceptSocket();

				System.Console.WriteLine("Der er gået en i fælden");

				NetworkStream netStream = new NetworkStream(clientSocket);
				StreamWriter writer = new StreamWriter (netStream);
				StreamReader reader = new StreamReader (netStream);

				writer.WriteLine("Welkommen - tast BYE for afslutning");
				writer.Flush();

				string text;
				text = reader.ReadLine();
				while ((text != null) && (! text.Trim().ToUpper().Equals("BYE") ) )
				{
					text = "ECHO: <"+text.ToUpper()+">";
					writer.WriteLine(text);
					writer.Flush();
					text = reader.ReadLine();
				}
				
				writer.Close();
				reader.Close();
				netStream.Close();
				clientSocket.Close();
			}
		}
	}
}
