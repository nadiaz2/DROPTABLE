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
			httpsv = new HttpServer(PORT);
			httpsv.AddWebSocketService<Echo>("/");

			httpsv.DocumentRootPath = "Assets/HTTP_Public";
			httpsv.AllowForwardedRequest = true;

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
				Debug.Log($"{Networks.FirstOrDefault().IPString}");
				Debug.Log($"Listening on port {httpsv.Port}, and providing WebSocket services:");

				foreach (var path in httpsv.WebSocketServices.Paths)
				{
					Debug.Log($"- {path}");
				}
			}

			running = true;

			return PORT;
		}

		public List<NetworkInterfaceInfo> Networks
		{
			get
			{
				List<NetworkInterfaceInfo> values = new List<NetworkInterfaceInfo>();

				foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
				{
					var properties = nic.GetIPProperties();

					// Checks if the network connection is up AND if the network interface has a default gateway
					if ((properties?.GatewayAddresses?.Count > 0) && (nic.OperationalStatus == OperationalStatus.Up))
					{
						foreach (var ip in properties.UnicastAddresses)
						{
							if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
							{
								//Debug.Log($"{nic.Name} {ip.Address.ToString()}");
								values.Add(new NetworkInterfaceInfo(nic.Name, ip.Address.ToString()));
							}
						}
					}
				}

				return values;
			}
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