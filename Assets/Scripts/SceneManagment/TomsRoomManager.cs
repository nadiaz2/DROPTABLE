using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum TomsRoomState
{
    Start,
    PlayingGame,
    GamePaused,

    //States for Day2
    StartDay2,
}

public class TomsRoomManager : MonoBehaviour
{

    public static TomsRoomState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //Start of Day 2
        if (GameManager.state == GameState.Day2StartTomsRoom)
        {
            //Send message to phone to send noticification from school on phone
        }
        else
        {
            //First time entering scene Day 1
            state = TomsRoomState.Start;
        }
    }
}
