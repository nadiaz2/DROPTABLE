using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum LivingRoomState
{
    Day1ReturnHome,
    Day1JacobsBackAfterBed,
    Day1TalkedWithJacob,
    Day1FoundPhoto,

    //Day 2 States
    Day2Start,
    Day2ReturnHome,
    Day2JacobsReturned,

    //Day 3 States
    Day3Start,
    Day3StartMinigame,
    Day3TalkedWithJacob,

    //Day 4 States
    Day4Start,
    Day4StartMinigame,
    Day4GoToLab,
}

public class LivingRoomManager : MonoBehaviour
{
    [Header("Characters")]
    public PlayerMovement player;
    public JacobLivingRoom jacob;
    public LivingRoomPhone phone;

    [Header("Spawn Points")]
    public Transform inFrontTomsDoor;
    public Transform inFrontJacobsDoor;
    public Transform offscreenPoint;

    [Header("Text Triggers")]
    public SubtitleTrigger reminderTrigger;
    public DialogueTrigger endGameTrigger;
    public DialogueTrigger continueToDay2Trigger;
    public SubtitleTrigger day2ReturnHomeTrigger;
    public SubtitleTrigger headToJacobsLabReminder;

    [Header("Chairs")]
    public Transform jacobSeat;
    public Transform tomSeat;

    private bool dialogueTriggerStarted;
    private bool dialogueTriggerStarted2;

    [Header("Black Screen")]
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

        switch (GameManager.lastScene)
        {
            case "TomsRoom":
                player.TeleportPlayer(inFrontTomsDoor);
                break;

            case "JacobsRoom":
                player.TeleportPlayer(inFrontJacobsDoor);
                break;
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
                break;

            case GameState.Day2HeadBackHome:
                state = LivingRoomState.Day2ReturnHome;
                jacob.gameObject.SetActive(true);
                jacob.transform.position = offscreenPoint.position;
                day2ReturnHomeTrigger.TriggerSubtitle(() =>
                {
                    phone.active = true;
                });
                break;

            case GameState.Day3StartTomsRoom:
                state = LivingRoomState.Day3Start;
                break;

            case GameState.Day4StartTomsRoom:
                state = LivingRoomState.Day4Start;
                break;

            case GameState.Day4HeadToJacobsLab:
                headToJacobsLabReminder.TriggerSubtitle(() =>
                {
                    state = LivingRoomState.Day4GoToLab;
                });
                break;

        }

        switch (LivingRoomManager.state)
        {
            case LivingRoomState.Day1TalkedWithJacob:
                jacob.gameObject.SetActive(false);
                jacob.interactable = false;
                break;
        }
    }


    private void Update()
    {
        switch (GameManager.state)
        {

            case GameState.Day1GameEndChoice:
                if (GameManager.day1BranchEndGame)
                {
                    if (!dialogueTriggerStarted)
                    {
                        endGameTrigger.TriggerDialogue(() =>
                        {
                            // Go to Toms Room then => Losing Screen/Bring up losing Screen
                            SceneManager.LoadScene("TomsRoom");
                            TomsRoomManager.state = TomsRoomState.Day1EndGame;
                        });
                        dialogueTriggerStarted = true;
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                        {
                            endGameTrigger.ContinueDialogue();
                        }
                    }
                }
                else if (!GameManager.day1BranchEndGame)
                {
                    if (!dialogueTriggerStarted2)
                    {
                        continueToDay2Trigger.TriggerDialogue(() =>
                        {
                            SceneManager.LoadScene("TomsRoom");
                            GameManager.state = GameState.Day2StartTomsRoom;
                        });
                        dialogueTriggerStarted2 = true;
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                        {
                            continueToDay2Trigger.ContinueDialogue();
                        }
                    }

                }
                break;

            case GameState.Day3FinishedMiniGame:
                jacob.gameObject.SetActive(true);
                jacob.interactable = true;
                break;

            case GameState.Day3TalkedWithJacob:
                jacob.gameObject.SetActive(false);
                jacob.interactable = false;
                break;

        }

        switch (LivingRoomManager.state)
        {
            case LivingRoomState.Day4Start:

                break;
        }
    }

    public void SitCharacters()
    {
        player.immobile = true;
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
