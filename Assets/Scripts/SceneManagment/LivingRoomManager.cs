using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum LivingRoomState
{
    Start,
    TalkingToJacob,
    FinishedTalking,
    PlayingGame,
	GamePaused,

    //Day 2 States
    Day2Start,
}

public class LivingRoomManager : MonoBehaviour
{
    public GameObject jacob;

    public static LivingRoomState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // Start of Day 2 going to class
        if (GameManager.state == GameState.Day2HeadBackToSchool)
        {
            state = LivingRoomState.Day2Start;
        }
        else
        {
            // Day 1 first time in livingroom
            state = LivingRoomState.Start;
        }
        
    }
}
