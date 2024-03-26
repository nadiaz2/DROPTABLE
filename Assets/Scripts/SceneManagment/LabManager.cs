using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public enum LabState
{

    // Day 4
    Day4InsideLab,

}

public class LabManager : MonoBehaviour
{
    public SubtitleTrigger reminder;
    public SubtitleTrigger subtitleTrigger;

    private bool subtitleTriggered;

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

            case GameState.Day4BackToPresent:
                morgan.SetActive(true);
                Jacob.SetActive(true);
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
