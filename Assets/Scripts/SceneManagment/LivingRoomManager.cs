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
    Day1TalkedWithJacob,

    //Day 2 States
    Day2Start,
    Day2ReturnHome,

    //Day 3 States
    Day3Start,
    Day3TalkedWithJacob,
}

public class LivingRoomManager : MonoBehaviour
{
    public GameObject player;
    public JacobLivingRoom jacob;
    public Transform inFrontTomsDoor;
    public SubtitleTrigger reminderTrigger;

    public Transform jacobSeat;
    public Transform tomSeat;


    public BlackScreen blackScreen;


    public static LivingRoomState state { get; set; }
    public static LivingRoomManager currentInstance
    {
        get
        {
            return _instance;
        }
    }
    private static LivingRoomManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        LivingRoomManager._instance = this;
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
                if (LivingRoomManager.state != LivingRoomState.Day1TalkedWithJacob)
                {
                    state = LivingRoomState.Day1JacobsBackAfterBed;
                    jacob.gameObject.SetActive(true);
                    jacob.interactable = true;
                }
                break;

            case GameState.Day2HeadBackToSchool:
                state = LivingRoomState.Day2Start;
                player.transform.position = new Vector3(205, 53, -516);
                break;

            case GameState.Day2HeadBackHome:
                state = LivingRoomState.Day2ReturnHome;
                break;

            case GameState.Day3StartTomsRoom:
                state = LivingRoomState.Day3Start;
                break;

        }

        switch (LivingRoomManager.state)
        {
            case LivingRoomState.Day1TalkedWithJacob:
                jacob.gameObject.SetActive(false);
                jacob.interactable = false;
                break;
        }
        Debug.Log($"Game: {GameManager.state}, Tom: {TomsRoomManager.state}, Living Room: {state}");
        
    }


    private void Update()
    {
        switch (GameManager.state)
        {
            case GameState.Day3FinishedMiniGame:
                jacob.gameObject.SetActive(true);
                jacob.interactable = true;
                break;

            case GameState.Day3TalkedWithJacob:
                jacob.gameObject.SetActive(false);
                jacob.interactable = false;
                break;
        }
    }

    public void SitCharacters()
    {
        player.transform.position = tomSeat.position;
        player.transform.rotation = tomSeat.rotation;
        jacob.transform.position = jacobSeat.position;
        jacob.transform.rotation = jacobSeat.rotation;
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
