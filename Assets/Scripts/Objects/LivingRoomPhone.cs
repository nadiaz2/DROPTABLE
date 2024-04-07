using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Linq;

using ZXing;
using ZXing.QrCode;

public class LivingRoomPhone : MonoBehaviour, Interactable
{
    [HideInInspector]
    public bool active;
    public static bool onPhone; //TODO: remove; currently needed by PlayerMovement2D

    // QRCode Fields
    public SubtitleTrigger photoFoundTrigger;
    public Animator animator;
    public Image phoneScreen;
    private Texture2D _storeEncodedTexture;


    // Start is called before the first frame update
    void Start()
    {
        _storeEncodedTexture = new Texture2D(256, 256);
        active = false;

        DisplayQRCode(Connection.RoomID);
        Connection.SetListener("01", (command) =>
        {
            switch (command)
            {
                case "FOUNDPHOTO":
                    photoFoundTrigger.TriggerSubtitle(() => {
                        animator.SetBool("isOpen", false);
                        LivingRoomManager.state = LivingRoomState.Day1FoundPhoto;
                    });
                    break;
                default:
                    Debug.Log($"Unkown command recieved: 01-{command}");
                    break;
            }
        });
    }

    public void DisplayQRCode(string roomID)
    {
        EncodeTextToQRCode($"https://echoes-through-the-screen-8aqb2.ondigitalocean.app/?id={roomID}");
    }

    private void EncodeTextToQRCode(string textWrite)
    {
        Color32[] _convertPixelToTexture = Encode(textWrite, _storeEncodedTexture.width, _storeEncodedTexture.height);
        _storeEncodedTexture.SetPixels32(_convertPixelToTexture);
        _storeEncodedTexture.Apply();

        phoneScreen.material.mainTexture = _storeEncodedTexture;
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
        Connection.MessagePhone("01-START");
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
