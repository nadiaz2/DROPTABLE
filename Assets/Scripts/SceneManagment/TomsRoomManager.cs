using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

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
    StartDay3,
    Day4GoToJacobsRoom,

}

public class TomsRoomManager : MonoBehaviour
{

    public TomsBed tomsBed;

    public SubtitleTrigger subtitleTrigger;
    public SubtitleTrigger subtitleTriggerDay4;

    public BlackScreen blackScreen;
    public DayText dayText;

    public static TomsRoomState state { get; set; }

    public static TomsRoomManager currentInstance
    {
        get
        {
            return _instance;
        }
    }
    private static TomsRoomManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        TomsRoomManager._instance = this;


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
                    dayText.SetText("Day 2");
                    Invoke("FadeIn", 1.5f);
                    Invoke("EraseDayText", 3f);
                    GameManager.day2Started = true;
                    Connection.MessagePhone("02-DIALOG");
                }

                //TODO Send message to phone to send noticification from school on phone


                //TODO Sends message back saying player has seen phone message (if statement)
                state = TomsRoomState.RachelDeathMessageSeen;
                break;

            case GameState.Day3StartTomsRoom:
                if (GameManager.day3Started)
                {
                    dayText.SetText("");
                    FadeIn();
                }
                else
                {
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
