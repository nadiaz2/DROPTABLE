using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public enum LabState
{

    // Day 4
    Day4InsideLab,
    Day4Run,

}

public class LabManager : MonoBehaviour
{
    public SubtitleTrigger reminder;
    public SubtitleTrigger subtitleTrigger;
    public DialogueTrigger backFromFlashbackDialogueTrigger;
    public DialogueTrigger confrontingTomDialogue;
    public SubtitleTrigger beforeMinigameSubtitleTrigger;
    public SubtitleTrigger runSubtitleTrigger;

    private bool subtitleTriggered;
    private bool backFromFlashbackTriggered;
    private bool confrontingTomTriggered;
    private bool runSubtitleTriggered;

    public GameObject morgan;
    public GameObject Jacob;
    public JacobsStation jacobsStation;

    public BlackScreen blackScreen;

    public static LabState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        blackScreen.goBlacked = false;

        switch (GameManager.state)
        {
            case GameState.Day4InLab:
                reminder.TriggerSubtitle(() =>
                {
                    jacobsStation.interactable = true;
                });
                break;

        }

    }

    private void Update()
    {
        switch (GameManager.state)
        {
            case GameState.Day4FinishedMiniGame:
                morgan.SetActive(true);
                Jacob.SetActive(true);
                if (!subtitleTriggered)
                {
                    subtitleTriggered = true;
                    subtitleTrigger.TriggerSubtitle(() =>
                    {
                        GameManager.state = GameState.Day4JacobFlashBack;
                        Invoke("delayedFlashBack", 2);
                    });
                }
                break;

            case GameState.Day4BackToPresent:
                morgan.SetActive(true);
                Jacob.SetActive(true);
                //TODO Spawn in front of Jacobs station
                if (!backFromFlashbackTriggered)
                {
                    backFromFlashbackDialogueTrigger.TriggerDialogue(() =>
                    {
                        morgan.SetActive(false);
                        GameManager.state = GameState.Day4ConfrontingTom;
                    });
                    backFromFlashbackTriggered = true;
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        backFromFlashbackDialogueTrigger.ContinueDialogue();
                    }
                }
                break;

            case GameState.Day4ConfrontingTom:
                if (!confrontingTomTriggered)
                {
                    confrontingTomDialogue.TriggerDialogue(() =>
                    {
                        beforeMinigameSubtitleTrigger.TriggerSubtitle(() =>
                        {
                            GameManager.state = GameState.Day4ThrowWaterMiniGame;
                        });
                    });
                    confrontingTomTriggered = true;
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        confrontingTomDialogue.ContinueDialogue();
                    }
                }
                break;

            case GameState.Day4ThrowWaterMiniGame:
                //TODO Quick time minigame with throwing cup of water then set game state
                GameManager.state = GameState.Day4FinishedTrowWaterMiniGame;
                break;

            case GameState.Day4FinishedTrowWaterMiniGame:
                if (!runSubtitleTriggered)
                {
                    runSubtitleTrigger.TriggerSubtitle();
                    runSubtitleTriggered = true;
                    state = LabState.Day4Run;
                }
                break;

        }
    }

    private void delayedFlashBack()
    {
        SceneManager.LoadScene("JacobsRoom");
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
