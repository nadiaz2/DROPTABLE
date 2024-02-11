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
    Day1JacobsBackAfterBed,

    //Day 2 States
    Day2Start,
    Day2ReturnHome,
}

public class LivingRoomManager : MonoBehaviour
{
    public GameObject player;
    public JacobDay2 jacob;
    public Transform inFrontTomsDoor;
    public SubtitleTrigger reminderTrigger;

    public BlackScreen blackScreen;


    public static LivingRoomState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        blackScreen.goBlacked = false;

        //Temp for testing
        if (GameManager.state == GameState.GameStart)
        {
            GameManager.state = GameState.ReturningHomeAfterHeadphones;
        }

        switch (GameManager.state)
        {
            // Day 1 first time in livingroom
            case GameState.ReturningHomeAfterHeadphones:
                GameManager.state = GameState.Day1LivingRoomStart;
                state = LivingRoomState.Day1ReturnHome;
                reminderTrigger.TriggerSubtitle();
                break;

            case GameState.Day1JacobsBack:
                state = LivingRoomState.Day1JacobsBackAfterBed;
                jacob.gameObject.SetActive(true);
                jacob.interactable = true;
                break;

            case GameState.Day2HeadBackToSchool:
                state = LivingRoomState.Day2Start;
                player.transform.position = new Vector3(205, 53, -516);
                break;

            case GameState.Day2HeadBackHome:
                state = LivingRoomState.Day2ReturnHome;
                break;

        }
        //Debug.Log($"Game: {GameManager.state}, Tom: {TomsRoomManager.state}, Living Room: {state}");
        
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
