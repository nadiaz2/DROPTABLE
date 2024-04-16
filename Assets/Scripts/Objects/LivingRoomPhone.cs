using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public PlayerMovement player;
    public Image phoneScreen;
    private Texture2D _storeEncodedTexture;

    [Header("Day 1 Subtitles")]
    public SubtitleTrigger photoFoundTrigger;

    [Header("Day 2 Subtitles")]
    public SubtitleTrigger albumOpenTrigger;
    public SubtitleTrigger browserOpenTrigger;
    public SubtitleTrigger chatOpenTrigger;
    public SubtitleTrigger keyChatFoundTrigger;

    [Header("Day 2 Dialogue")]
    public DialogueTrigger day2ReturnDialogue;


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
                /*
                    keyChatFoundTrigger.TriggerSubtitle(() =>
                    {
                        animator.SetBool("isOpen", false);
                        // Player goes into dialogue. Will be mobile again afterwards
                        //player.immobile = false;
                        LivingRoomManager.state = LivingRoomState.Day2JacobsReturned;
                    });
                    */
                    break;

                case "FINISH":
                    keyChatFoundTrigger.TriggerSubtitle(() =>
                    {
                        animator.SetBool("isOpen", false);
                        // Player goes into dialogue. Will be mobile again afterwards
                        //player.immobile = false;
                        LivingRoomManager.state = LivingRoomState.Day2JacobsReturned;

                        active = true;
                        day2ReturnDialogue.TriggerDialogue(() =>
                        {
                            LivingRoomManager.currentInstance.FadeOut();
                            Invoke("EndDay2", 1.0f);
                        });
                    });
                    break;

                default:
                    Debug.Log($"Unkown command recieved: 02-{command}");
                    break;
            }
        });
    }

    private void EndDay2()
    {
        GameManager.state = GameState.Day3StartTomsRoom;
        SceneManager.LoadScene("TomsRoom");
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
        if(LivingRoomManager.state == LivingRoomState.Day2JacobsReturned)
        {
            day2ReturnDialogue.ContinueDialogue();
            return;
        }

        animator.SetBool("isOpen", true);
        this.active = false;

        switch(LivingRoomManager.state)
        {
            case LivingRoomState.Day1TalkedWithJacob:
                Connection.MessagePhone("01-START");
                break;
                
            case LivingRoomState.Day2ReturnHome:
                Connection.MessagePhone("02-START");
                player.immobile = true;
                break;
        }
    }

    public string GetPrompt()
    {
        // The Phone controls the dialogue for day 2 since the player's position can vary.
        return (LivingRoomManager.state == LivingRoomState.Day2JacobsReturned) ?
                "Talk to Jacob" :
                "Jacob's Phone";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
