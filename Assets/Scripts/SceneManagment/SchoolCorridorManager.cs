using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum SchoolCorridorState
{

    // Day 4
    Day4CheckJacobsLab,
    Day4TryToOpenDoor,
    Day4MorganHere,
    Day4LabDoorsOpen,

}

public class SchoolCorridorManager : MonoBehaviour
{
    public SubtitleTrigger subtitleTrigger;
    public DialogueTrigger dialogueTrigger;

    public GameObject morgan;

    private bool subtitleplayed = false;
    private bool dialogueTriggerStarted = false;

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
