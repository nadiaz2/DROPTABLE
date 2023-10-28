using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

using WebSockets;
using System.Linq;

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
        SocketServer server = SocketServer.Instance;
        int port = server.StartServer();
        NetworkInterfaceInfo interfaceInfo = server.Networks.FirstOrDefault();
        EncodeTextToQRCode($"http://{interfaceInfo.IPString}:{port}/");
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
}
