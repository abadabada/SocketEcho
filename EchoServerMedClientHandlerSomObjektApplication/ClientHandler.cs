using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace EchoServerParallelApplication
{
	public class ClientHandler
	{
		private Socket clientSocket;

		public ClientHandler(Socket clientSocket)
		{
			this.clientSocket = clientSocket;
		}

		public void RunClient ()
		{
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
