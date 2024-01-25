using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
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

    public static bool onPhone = false;
    public static bool wasPaused = false;

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
        if(wasPaused)
        {
            finalPosition = camera.transform.position + camera.transform.forward * 10f;
            finalRotation = camera.transform.rotation * Quaternion.AngleAxis(180, Vector3.up);
            wasPaused = false;
        }

        if (PhoneFoundDialogue.photoFound || GameManager.state == GameState.GamePaused)
        {
            this.transform.position = Vector3.Lerp(startPosition, finalPosition, lerpPercent);
            this.transform.rotation = Quaternion.Slerp(startRotation, finalRotation, lerpPercent);
            lerpPercent = Math.Max(lerpPercent - 0.005f, 0.0f);
            onPhone = false;
        }
        else if(GameManager.state == GameState.PlayingGame)
        {
            this.transform.position = Vector3.Lerp(startPosition, finalPosition, lerpPercent);
            this.transform.rotation = Quaternion.Slerp(startRotation, finalRotation, lerpPercent);
            lerpPercent = Math.Min(lerpPercent + 0.005f, 1.0f);
            onPhone = true;
        }
        else if(phoneMoving)
        {
            this.transform.position = Vector3.Lerp(startPosition, finalPosition, lerpPercent);
            this.transform.rotation = Quaternion.Slerp(startRotation, finalRotation, lerpPercent);
            lerpPercent = Math.Min(lerpPercent + 0.005f, 1.0f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit, 75f))
            {
                if (Hit.collider.gameObject.CompareTag("Phone") && (GameManager.state == GameState.FinishedTalking))
                {
                    phoneMoving = true;
                    finalPosition = camera.transform.position + camera.transform.forward * 10f;
                    finalRotation = camera.transform.rotation * Quaternion.AngleAxis(180, Vector3.up);
                    onPhone = true;
                    DisplayQRCode();
                }
            }
        }
    }

    private void DisplayQRCode()
    {
        //SocketServer server = SocketServer.Instance;
        //NetCoreWebsockets server = NetCoreWebsockets.Instance;
        //int port = server.StartServer();
        int port = 8000;
        NetworkInterfaceInfo interfaceInfo = Utility.Networks.FirstOrDefault();
        EncodeTextToQRCode($"https://{interfaceInfo.IPString}:{port}/before.html");
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
