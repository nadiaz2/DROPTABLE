using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;

using ZXing;
using ZXing.QrCode;

public enum TomsRoomState
{
    Start,
    PlayingGame,
    GamePaused,

    // Day 1
    Day1Start,
    Day1JacobsBack,
    Day1EndGame,


    // States for Day2
    StartDay2,
    RachelDeathMessageSeen,


    // States for Day 3
    BeforeSubtitleEnd,
    StartDay3,
    Day4GoToJacobsRoom,

}

public class TomsRoomManager : MonoBehaviour
{
    [Header("World Objects")]
    public TomsBed tomsBed;
    public BlackScreen blackScreen;
    public DayText dayText;
    public Animator phoneAnimator;
    public Image phoneScreen;
    private Texture2D _storeEncodedTexture;

    [Header("Day 2 Subtitles")]
    public SubtitleTrigger wakeUpTrigger;
    public SubtitleTrigger afterEmailTrigger;

    [Header("Day 3 Subtitles")]
    public SubtitleTrigger subtitleTrigger;

    [Header("Day 4 Subtitles")]
    public SubtitleTrigger subtitleTriggerDay4;


    public static TomsRoomState state { get; set; }

    private static TomsRoomManager _instance;
    public static TomsRoomManager currentInstance
    {
        get
        {
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TomsRoomManager._instance = this;

        //DisplayQRCode(Connection.RoomID);
        Connection.SetListener("02", (command) =>
        {
            switch (command)
            {
                case "DIALOGCLOSED":
                    phoneAnimator.SetBool("isOpen", false);
                    afterEmailTrigger.TriggerSubtitle(() => {
                        state = TomsRoomState.RachelDeathMessageSeen;
                    });
                    break;
                default:
                    Debug.Log($"Unkown command recieved: 02-{command}");
                    break;
            }
        });


        switch (GameManager.state)
        {
            case GameState.Day1LivingRoomStart:
                state = TomsRoomState.Day1Start;
                tomsBed.interactable = true;
                FadeIn();
                break;

            //Start of Day 2
            case GameState.Day2StartTomsRoom:
                if(GameManager.day2Started)
                {
                    dayText.SetText("");
                    FadeIn();
                }
                else
                {
                    TomsRoomManager.state = TomsRoomState.StartDay2;
                    dayText.SetText("Day 2");
                    Invoke("FadeIn", 1.5f);
                    Invoke("EraseDayText", 3f);
                    GameManager.day2Started = true;

                    wakeUpTrigger.TriggerSubtitle(() => {
                        phoneAnimator.SetBool("isOpen", true);
                        Connection.MessagePhone("02-DIALOG");
                    });
                    //Phone will send message back when the player has seen the dialog
                }
                break;

            case GameState.Day3StartTomsRoom:
                if (GameManager.day3Started)
                {
                    dayText.SetText("");
                    FadeIn();
                }
                else
                {
                    TomsRoomManager.state = TomsRoomState.BeforeSubtitleEnd;
                    dayText.SetText("Day 3");
                    Invoke("FadeIn", 1.5f);
                    Invoke("EraseDayText", 3f);
                    GameManager.day3Started = true;
                }

                if (TomsRoomManager.state != TomsRoomState.StartDay3)
                {
                    subtitleTrigger.TriggerSubtitle(() =>
                    {
                        state = TomsRoomState.StartDay3;
                    });
                }
                break;

            case GameState.Day4StartTomsRoom:
                if (GameManager.day4Started)
                {
                    dayText.SetText("");
                    FadeIn();
                }
                else
                {
                    dayText.SetText("Day 4");
                    Invoke("FadeIn", 1.5f);
                    Invoke("EraseDayText", 3f);
                    GameManager.day4Started = true;
                }
                Invoke("delayedDay4Dialogue", 3.5f);
                break;

            default:
                FadeIn();
                break;
        }

        switch (TomsRoomManager.state)
        {
            case TomsRoomState.Day1EndGame:
                // Testing to end the game
                //EditorApplication.Exit(0);
                Debug.Log("Die");
                break;
        }

        

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

    private void delayedDay4Dialogue()
    {
        subtitleTriggerDay4.TriggerSubtitle(() =>
        {
            state = TomsRoomState.Day4GoToJacobsRoom;
        });
    }


    public void FadeOut()
    {
        if (blackScreen != null)
        {
            blackScreen.goBlacked = true;
        } 

        if (dayText != null)
        {
            dayText.goInvisible = false;
        }
    }

    public void FadeIn()
    {
        if (blackScreen != null)
        {
            blackScreen.goBlacked = false;
        }

        if (dayText != null)
        {
            dayText.goInvisible = true;
        }
    }

    private void EraseDayText()
    {
        dayText.SetText("");
    }
}
