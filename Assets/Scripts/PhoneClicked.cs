using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

using WebSockets;
using System.Linq;

using ZXing;
using ZXing.QrCode;

public class PhoneClicked : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 finalPosition;
    private Quaternion finalRotation;
    private float lerpPercent;
    private bool phoneMoving;

    private GameObject camera;

    // QRCode Fields
    private Texture2D _storeEncodedTexture;
    public GameObject phoneScreen;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;
        lerpPercent = 0.0f;
        phoneMoving = false;

        camera = GameObject.Find("PlayerCamera");

        _storeEncodedTexture = new Texture2D(256, 256);
    }

    // Update is called once per frame
    void Update()
    {
        if(phoneMoving) {
            this.transform.position = Vector3.Lerp(startPosition, finalPosition, lerpPercent);
            this.transform.rotation = Quaternion.Slerp(startRotation, finalRotation, lerpPercent);
            lerpPercent = Math.Min(lerpPercent + 0.005f, 1.0f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit))
            {
                if (Hit.collider.gameObject.tag == "Phone")
                {
                    phoneMoving = true;
                    finalPosition = camera.transform.position + camera.transform.forward * 10f;

                    Vector3 rot = camera.transform.rotation.eulerAngles;
                    finalRotation = camera.transform.rotation * Quaternion.AngleAxis(180, Vector3.up);

                    DisplayQRCode();
                }
            }
        }
    }

    private void DisplayQRCode()
    {
        SocketServer server = SocketServer.Instance;
        int port = server.StartServer();
        NetworkInterfaceInfo interfaceInfo = server.Networks.FirstOrDefault();
        EncodeTextToQRCode($"http://{interfaceInfo.IPString}:{port}/before.html");
    }

    private void EncodeTextToQRCode(string textWrite)
    {
        //string textWrite = string.IsNullOrEmpty(_textInputField.text) ? "Please give what you want to encode" : _textInputField.text;
        
        Color32[] _convertPixelToTexture = Encode(textWrite, _storeEncodedTexture.width, _storeEncodedTexture.height);
        _storeEncodedTexture.SetPixels32(_convertPixelToTexture);
        _storeEncodedTexture.Apply();

        phoneScreen.GetComponent<Renderer>().material.mainTexture = _storeEncodedTexture;
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
}
