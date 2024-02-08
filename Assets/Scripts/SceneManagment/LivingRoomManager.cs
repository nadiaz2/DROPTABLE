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

    Day1ReturnHome,

    //Day 2 States
    Day2Start,
    Day2ReturnHome,
}

public class LivingRoomManager : MonoBehaviour
{
    public GameObject player;
    public GameObject jacob;
    public Transform inFrontTomsDoor;

    public static LivingRoomState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        switch (GameManager.state)
        {
            // Day 1 first time in livingroom
            case GameState.ReturningHomeAfterHeadphones:
                GameManager.state = GameState.Day1LivingRoomStart;
                state = LivingRoomState.Day1ReturnHome;
                break;

            case GameState.Day2HeadBackToSchool:
                //Debug.Log(GameManager.state);
                state = LivingRoomState.Day2Start;
                player.transform.position = new Vector3(205, 53, -516);
                break;

            case GameState.Day2HeadBackHome:
                state = LivingRoomState.Day2ReturnHome;
                break;

        }
        
    }
}
