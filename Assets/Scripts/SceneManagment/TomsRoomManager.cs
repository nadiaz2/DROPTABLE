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

    // States for Day2
    StartDay2,
    RachelDeathMessageSeen,

}

public class TomsRoomManager : MonoBehaviour
{

    public static TomsRoomState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        //TODO Temp for testing day 2
        //GameManager.state = GameState.Day2StartTomsRoom;

        //Start of Day 2

        switch (GameManager.state)
        {
            case GameState.Day1LivingRoomStart:
                state = TomsRoomState.Day1Start;
                break;

            case GameState.Day2StartTomsRoom:
                //TODO Send message to phone to send noticification from school on phone


                //TODO Sends message back saying player has seen phone message (if statement)
                state = TomsRoomState.RachelDeathMessageSeen;
                break;

            
        }

    }
}
