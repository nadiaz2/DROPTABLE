using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using NetCoreServer;

using UnityEngine;

namespace WebSockets
{
	public class NetCoreWebsockets
	{
		private const int port = 3000 ;
        public static NetCoreWebsockets Instance { get; } = new NetCoreWebsockets();

        private SslContext context;
		private Server server;

		private NetCoreWebsockets()
		{
			// Create and prepare a new SSL server context
			context = new SslContext(
				SslProtocols.Tls12,
				new X509Certificate2("Assets/Certificate/Droptable.pfx", "123456")
			);

			// Create a new WebSocket server
			server = new Server(context, IPAddress.Any, port);
			server.AddStaticContent("Assets/HTTP_Public", "/");
		}

		public int StartServer()
		{
			Debug.Log("Starting server...");
			server.Start();
			return port;
		}

		public void StopServer()
		{
			Debug.Log("Stopping server...");
			server.Stop();
		}
	}

	class Server : WssServer
	{
		public Server(SslContext context, IPAddress address, int port) : base(context, address, port) { }

		protected override SslSession CreateSession() { return new Session(this); }

		protected override void OnError(SocketError error)
		{
			Console.WriteLine($"WebSocket server caught an error with code {error}");
		}
	}

	class Session : WssSession
	{
		public Session(WssServer server) : base(server) { }

		public override void OnWsConnected(HttpRequest request)
		{
			Console.WriteLine($"WebSocket session with Id {Id} connected!");

			// Send invite message
			//string message = "Hello from WebSocket chat! Please send a message or '!' to disconnect the client!";
			//SendTextAsync(message);
		}

		public override void OnWsDisconnected()
		{
			Console.WriteLine($"WebSocket session with Id {Id} disconnected!");
		}

		public override void OnWsReceived(byte[] buffer, long offset, long size)
		{
			string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
			Console.WriteLine("Incoming: " + message);

			// Multicast message to all connected sessions
			((WssServer)Server).MulticastText(message);

			// If the buffer starts with '!' the disconnect the current session
			if (message == "!")
				Close(1000);
		}

		protected override void OnError(SocketError error)
		{
			Console.WriteLine($"WebSocket session caught an error with code {error}");
		}
	}
}