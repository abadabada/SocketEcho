using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace EchoServerParallelApplication
{
    class Program
    {
		static void Main(string[] args)
		{
			EchoServerParallel server = new EchoServerParallel(12000);
            server.Run();
		}
	}

	class EchoServerParallel
	{
        private IPAddress ip = IPAddress.Parse("127.0.0.1");
        private int port;
        private volatile bool stop = false;

        public EchoServerParallel(int port)
        {
            this.ip = IPAddress.Parse("127.0.0.1");
            this.port = port;
        }

        public void Run()
		{
			System.Console.WriteLine("Echo server startet p� port:"+port);
			
			TcpListener listener =	new TcpListener(ip, port);
			listener.Start();
			while (!stop)
			{
				System.Console.WriteLine("Echo server klar");

				Socket clientSocket = listener.AcceptSocket();

				System.Console.WriteLine("Der er g�et en i f�lden");

				ClientHandler handler = new ClientHandler(clientSocket);
//				handler.RunClient();		// Kun een af gangen
								
				// Her s�ttes op til af behandling foreg�r parallel
				// s� der kan forts�ttes med en ny
                Thread clientTr�d = new Thread(handler.RunClient);
				clientTr�d.Start();
			}
		}
	}
}
