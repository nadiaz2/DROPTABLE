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

    // QRCode Fields
    [Header("World Objects")]
    public Animator animator;
    public Image phoneScreen;
    private Texture2D _storeEncodedTexture;

    [Header("Day 1 Subtitles")]
    public SubtitleTrigger photoFoundTrigger;

    [Header("Day 2 Subtitles")]
    public SubtitleTrigger albumOpenTrigger;
    public SubtitleTrigger browserOpenTrigger;
    public SubtitleTrigger chatOpenTrigger;
    public SubtitleTrigger keyChatFoundTrigger;


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
                    photoFoundTrigger.TriggerSubtitle(() =>
                    {
                        animator.SetBool("isOpen", false);
                        LivingRoomManager.state = LivingRoomState.Day1FoundPhoto;
                    });
                    break;
                default:
                    Debug.Log($"Unkown command recieved: 01-{command}");
                    break;
            }
        });

        Connection.SetListener("02", (command) =>
        {
            switch (command)
            {
                case "ALBUM":
                    albumOpenTrigger.TriggerSubtitle();
                    break;

                case "BROWSER":
                    browserOpenTrigger.TriggerSubtitle();
                    break;

                case "CHATAPP":
                    chatOpenTrigger.TriggerSubtitle();
                    break;

                case "FOUNDCHAT":
                    keyChatFoundTrigger.TriggerSubtitle();
                    break;

                case "FINISH":
                    break;

                default:
                    Debug.Log($"Unkown command recieved: 02-{command}");
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

        switch(LivingRoomManager.state)
        {
            case LivingRoomState.Day1TalkedWithJacob:
                Connection.MessagePhone("START01");
                break;
                
            case LivingRoomState.Day2ReturnHome:
                Connection.MessagePhone("START02");
                break;
        }
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
