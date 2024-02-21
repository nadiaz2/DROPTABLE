using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

using ZXing;
using ZXing.QrCode;

public class PhoneClicked : MonoBehaviour, Interactable
{
    public bool active;
    public static bool onPhone; //TODO: remove

    // QRCode Fields
    public Animator animator;
    public GameObject phoneScreen;
    private Texture2D _storeEncodedTexture;


    // Start is called before the first frame update
    void Start()
    {
        _storeEncodedTexture = new Texture2D(256, 256);
        active = true;

        SocketIOUnity Socket = SocketClient.Socket;
        Socket.On("01", (response) =>
        {
            string command = response.GetValue<string>();
            switch (command)
            {
                case "FOUNDPHOTO":
                    animator.SetBool("isOpen", false);
                    break;
                default:
                    Debug.Log($"Unkown command recieved: 01-{response}");
                    break;
            }
        });

        DisplayQRCode();
    }

    private void DisplayQRCode()
    {
        string roomID = SocketClient.RoomID;
        EncodeTextToQRCode($"https://echoes-through-the-screen-8aqb2.ondigitalocean.app/room?id={roomID}");
    }

    private void EncodeTextToQRCode(string textWrite)
    {
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

    public void Interact()
    {
        animator.SetBool("isOpen", true);
        this.active = false;
    }

    public string GetPrompt()
    {
        return "Jacob's Phone";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
