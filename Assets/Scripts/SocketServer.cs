// Websocket_Sharp Libraries
using WebSocketSharp.Net;
using WebSocketSharp.Server;
using WebSocketBehaviors;

using System.Text;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;

using UnityEngine;
using System.Security.Cryptography.X509Certificates;

namespace WebSockets
{
	public readonly struct NetworkInterfaceInfo
	{
		public NetworkInterfaceInfo(string name, string ip)
		{
			InterfaceName = name;
			IPString = ip;
		}

		public string InterfaceName { get; }
		public string IPString { get; }
	}

	class Utility
	{
		public static List<NetworkInterfaceInfo> Networks
		{
			get
			{
				var values = new List<NetworkInterfaceInfo>();

				// All interfaces that are up and have a default gateway
				var validInterfaces =
					NetworkInterface.GetAllNetworkInterfaces()
					.Where((nic) => nic.OperationalStatus == OperationalStatus.Up)
					.Where((nic) => nic.GetIPProperties()?.GatewayAddresses?.Count > 0);

				foreach (var nic in validInterfaces)
				{
					var validIP = nic.GetIPProperties().UnicastAddresses
						.Where((ip) => ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
						.Select((ip) => ip.Address.ToString())
						.DefaultIfEmpty("0.0.0.0").First();

					//Debug.Log($"{nic.Name} {validIP.Address.ToString()}");
					values.Add(new NetworkInterfaceInfo(nic.Name, validIP));
				}

				return values;
			}
		}
	}

	public sealed class SocketServer
	{
		private static readonly SocketServer instance = new SocketServer();
		public static SocketServer Instance
		{
			get
			{
				return instance;
			}
		}

		const int PORT = 3000;

		private static bool running = false;
		private HttpServer httpsv;

		private SocketServer()
		{
			httpsv = new HttpServer(PORT, true);
			httpsv.AddWebSocketService<HandleMessage>("/");
			httpsv.SslConfiguration.ServerCertificate = new X509Certificate2("Assets/Certificate/Droptable.pfx", "123456");


			httpsv.DocumentRootPath = "Assets/HTTP_Public";
			//httpsv.AllowForwardedRequest = true;

			SetHTTPGet();
		}


		public int StartServer()
		{
			if (running)
			{
				return PORT;
			}

			httpsv.Start();

			// Print info about server to console
			if (httpsv.IsListening)
			{
				Debug.Log($"{Utility.Networks.FirstOrDefault().IPString}");
				Debug.Log($"Listening on port {httpsv.Port}, and providing WebSocket services:");

				foreach (var path in httpsv.WebSocketServices.Paths)
				{
					Debug.Log($"- {path}");
				}
			}

			running = true;

			return PORT;
		}



		private void SetHTTPGet()
		{
			httpsv.OnGet += (sender, e) =>
			{
				var req = e.Request;
				var res = e.Response;

				var path = req.RawUrl;

				if (path == "/")
				{
					path += "index.html";
				}

				byte[] contents;

				if (!e.TryReadFile(path, out contents))
				{
					res.StatusCode = (int)WebSocketSharp.Net.HttpStatusCode.NotFound;
					return;
				}

				if (path.EndsWith(".html"))
				{
					res.ContentType = "text/html";
					res.ContentEncoding = Encoding.UTF8;
				}
				else if (path.EndsWith(".js"))
				{
					res.ContentType = "application/javascript";
					res.ContentEncoding = Encoding.UTF8;
				}

				res.ContentLength64 = contents.LongLength;

				res.Close(contents, true);
			};
		}
	}
}