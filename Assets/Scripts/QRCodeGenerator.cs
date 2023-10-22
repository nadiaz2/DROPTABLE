using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

// Websocket_Sharp Libraries
using System.Text; //provides Encoding.UTF8
using WebSocketSharp.Net;
using WebSocketSharp.Server;
using My_Websocket;

// IP Address libraries
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

public class QRCodeGenerator : MonoBehaviour
{

    [SerializeField] private RawImage _rawImageReciever;

    private Texture2D _storeEncodedTexture;


    // Start is called before the first frame update
    void Start()
    {
        _storeEncodedTexture = new Texture2D(256, 256);
    }

    private Color32[] Encode(string textForEncoding, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width,
            }
        };
        return writer.Write(textForEncoding);
    }

    public void OnClickEncode()
    {
        int port = createWebsocketServer();
        EncodeTextToQRCode($"http://{GetLocalIPAddress()}:{port}/");
    }

    private void EncodeTextToQRCode(string textWrite)
    {
        //string textWrite = string.IsNullOrEmpty(_textInputField.text) ? "Please give what you want to encode" : _textInputField.text;
        //string textWrite = "http://a4-bright183.glitch.me/";

        Color32[] _convertPixelToTexture = Encode(textWrite, _storeEncodedTexture.width, _storeEncodedTexture.height);
        _storeEncodedTexture.SetPixels32(_convertPixelToTexture);
        _storeEncodedTexture.Apply();

        _rawImageReciever.texture = _storeEncodedTexture;
    }

    private int createWebsocketServer()
    {
        const int PORT = 3000;

        var httpsv = new HttpServer(PORT);
        httpsv.AddWebSocketService<Echo>("/");

        httpsv.DocumentRootPath = "Assets/HTTP_Public";
        httpsv.AllowForwardedRequest = true;

        httpsv.OnGet += (sender, e) =>
        {
            var req = e.Request;
            var res = e.Response;

            var path = req.RawUrl;

            if (path == "/")
                path += "index.html";

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

        httpsv.Start();

        // Print info about server to console
        if (httpsv.IsListening)
        {
            Debug.Log($"{GetLocalIPAddress()}");
            Debug.Log($"Listening on port {httpsv.Port}, and providing WebSocket services:");

            foreach (var path in httpsv.WebSocketServices.Paths)
                Debug.Log($"- {path}");
        }

        return PORT;
    }

    public string GetLocalIPAddress()
    {
        //https://stackoverflow.com/questions/9855230/how-do-i-get-the-network-interface-and-its-right-ipv4-address

        var ips = NetworkInterface.GetAllNetworkInterfaces()
            .Select(i => i.GetIPProperties().UnicastAddresses)
            .SelectMany(u => u)
            .Where(u => u.Address.AddressFamily == AddressFamily.InterNetwork)
            .Select(i => i.Address);

        foreach (var ip in ips)
        {
            if (ip.ToString().Contains("192.168."))
            {
                return ip.ToString();
            }
        }

        return "";
    }

}
