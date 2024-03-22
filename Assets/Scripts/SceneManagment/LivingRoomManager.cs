using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.SceneManagement;

public enum LivingRoomState
{/*
    Start,
    TalkingToJacob,
    FinishedTalking,
    PlayingGame,
	GamePaused,
*/
    Day1ReturnHome,
    Day1JacobsBackAfterBed,
    Day1TalkedWithJacob,
    Day1FoundPhoto,

    //Day 2 States
    Day2Start,
    Day2ReturnHome,

    //Day 3 States
    Day3Start,
    Day3StartMinigame,
    Day3TalkedWithJacob,

    //Day 4 Stats
    Day4Start,
    Day4StartMinigame,
}

public class LivingRoomManager : MonoBehaviour
{
    public PlayerMovement player;
    public JacobLivingRoom jacob;
    public Transform inFrontTomsDoor;
    public SubtitleTrigger reminderTrigger;
    public DialogueTrigger endGameTrigger;
    public DialogueTrigger continueToDay2Trigger;
    public SubtitleTrigger headToJacobsLabReminder;

    public Transform jacobSeat;
    public Transform tomSeat;

    private bool dialogueTriggerStarted;
    private bool dialogueTriggerStarted2;

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
                //player.transform.position = new Vector3(205, 53, -516);
                break;

            case GameState.Day2HeadBackHome:
                state = LivingRoomState.Day2ReturnHome;
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
                    //TODO Enable the door to go outside
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
                }else if (!GameManager.day1BranchEndGame)
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
