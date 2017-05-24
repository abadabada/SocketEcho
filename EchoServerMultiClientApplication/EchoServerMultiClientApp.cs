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
			System.Console.WriteLine("Echo server startet på port:"+port);
			
			TcpListener listener =	new TcpListener(ip, port);
			listener.Start();
			while (!stop)
			{
				System.Console.WriteLine("Echo server klar");

				Socket clientSocket = listener.AcceptSocket();

				System.Console.WriteLine("Der er gået en i fælden");

				ClientHandler handler = new ClientHandler(clientSocket);
//				handler.RunClient();		// Kun een af gangen
								
				// Her sættes op til af behandling foregår parallel
				// så der kan fortsættes med en ny
                Thread clientTråd = new Thread(handler.RunClient);
				clientTråd.Start();
			}
		}
	}
}
