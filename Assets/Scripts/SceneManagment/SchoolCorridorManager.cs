using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.SceneManagement;

public enum SchoolCorridorState
{

    // Day 4
    Day4CheckJacobsLab,
    Day4TryToOpenDoor,
    Day4MorganHere,
    Day4LabDoorsOpen,
    Day4FlashBack,
    Day4FlashBackTalkToMorgan,
    Day4Run,

}

public class SchoolCorridorManager : MonoBehaviour
{
    public SubtitleTrigger subtitleTrigger;
    public DialogueTrigger dialogueTrigger;
    public DialogueTrigger talkToMorganTrigger;
    public SubtitleTrigger runAwayReminderTrigger;

    public GameObject player;
    public GameObject morgan;
    public GameObject jacobPlayable;
    public GameObject morganInteractable;

    private bool subtitleplayed = false;
    private bool dialogueTriggerStarted = false;
    private bool dialogueTriggerStartedMorgan = false;

    public BlackScreen blackScreen;

    public static SchoolCorridorState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        blackScreen.goBlacked = false;


        switch (GameManager.state)
        {
            case GameState.Day4HeadToJacobsLab:
                state = SchoolCorridorState.Day4CheckJacobsLab;
                break;

            case GameState.Day4JacobFlashBack:
                jacobPlayable.SetActive(true);
                player.SetActive(false);
                morganInteractable.SetActive(true);
                break;

            case GameState.Day4Run:
                runAwayReminderTrigger.TriggerSubtitle(() =>
                {

                });
                break;
        }

    }

    private void Update()
    {
        switch (SchoolCorridorManager.state)
        {
            case SchoolCorridorState.Day4TryToOpenDoor:
                if (!subtitleplayed)
                {
                    subtitleTrigger.TriggerSubtitle(() =>
                    {
                        state = SchoolCorridorState.Day4MorganHere;
                    });
                    subtitleplayed = true;
                }
                break;

            case SchoolCorridorState.Day4MorganHere:
                if (!dialogueTriggerStarted)
                {
                    morgan.SetActive(true);
                    dialogueTrigger.TriggerDialogue(() =>
                    {
                        state = SchoolCorridorState.Day4LabDoorsOpen;
                        morgan.SetActive(false);
                    });
                    dialogueTriggerStarted = true;
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        dialogueTrigger.ContinueDialogue();
                    }
                }
                break;

            case SchoolCorridorState.Day4FlashBack:
                break;

            case SchoolCorridorState.Day4FlashBackTalkToMorgan:
                if (!dialogueTriggerStartedMorgan)
                {
                    talkToMorganTrigger.TriggerDialogue(() =>
                    {
                        SceneManager.LoadScene("Lab");
                        GameManager.state = GameState.Day4BackToPresent;
                    });
                    dialogueTriggerStartedMorgan = true;
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        talkToMorganTrigger.ContinueDialogue();
                    }
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
