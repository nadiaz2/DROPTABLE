using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum TomsRoomState
{
    Start,
    PlayingGame,
    GamePaused,

    // Day 1
    Day1Start,
    Day1JacobsBack,


    // States for Day2
    StartDay2,
    RachelDeathMessageSeen,


    // States for Day 3
    StartDay3,

}

public class TomsRoomManager : MonoBehaviour
{

    public TomsBed tomsBed;

    public SubtitleTrigger subtitleTrigger;

    public BlackScreen blackScreen;

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

        blackScreen.goBlacked = false;

        //TODO Temp for testing day 2 & Day 3
        if (GameManager.state == GameState.GameStart)
        {
            //GameManager.state = GameState.Day2StartTomsRoom;
            GameManager.state = GameState.Day3StartTomsRoom;
        }

        //Start of Day 2

        switch (GameManager.state)
        {
            case GameState.Day1LivingRoomStart:
                state = TomsRoomState.Day1Start;
                tomsBed.interactable = true;
                break;


            case GameState.Day2StartTomsRoom:
                //TODO Send message to phone to send noticification from school on phone


                //TODO Sends message back saying player has seen phone message (if statement)
                state = TomsRoomState.RachelDeathMessageSeen;
                break;

            case GameState.Day3StartTomsRoom:
                if(TomsRoomManager.state != TomsRoomState.StartDay3)
                {
                    subtitleTrigger.TriggerSubtitle(() =>
                    {
                        state = TomsRoomState.StartDay3;
                    });
                }
                break;


        }

    }


    public void FadeOut()
    {
        if (blackScreen != null)
        {
            blackScreen.goBlacked = true;
        }
    }

    public void FadeIn()
    {
        if (blackScreen != null)
        {
            blackScreen.goBlacked = false;
        }
    }
}
