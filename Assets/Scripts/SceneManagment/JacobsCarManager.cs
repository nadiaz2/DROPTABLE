using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public enum JacobsCarState
{
    Start,
    PlayingGame,
    GamePaused,

    // Day 3 States
    Day3GoingToBackBay,
    Day3SkipToBackBay,
    Day3GoingHome,

}

public class JacobsCarManager : MonoBehaviour
{
    public SubtitleTrigger trigger;
    public SubtitleTrigger trigger2;
    public SubtitleTrigger trigger3;
    public SubtitleTrigger trigger4;
    public ChoiceTrigger day3Choice;

    public Text prompt;

    public BlackScreen blackScreen;

    public static JacobsCarState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.cameraMove = true;

        blackScreen.goBlacked = false;

        if (GameManager.state == GameState.GameStart)
        {
            GameManager.state = GameState.Day3End;
        }


        switch (GameManager.state)
        {
            case GameState.Day3TalkedWithJacob:
                state = JacobsCarState.Day3GoingToBackBay;
                Invoke("delayedSubtitlePlay", 5);
                break;

            case GameState.Day3End:
                state = JacobsCarState.Day3GoingHome;
                Invoke("delayedGoingHomeSubtitlePlay", 3);
                break;
        }
    }

    private void Update()
    {

        switch (JacobsCarManager.state)
        {
            case JacobsCarState.Day3SkipToBackBay:
                if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("BackBay");
                    prompt.text = "";
                    GameManager.state = GameState.Day3InBackBay;
                }
                break;
        }
    }

    private void delayedSubtitlePlay()
    {
        trigger.TriggerSubtitle(() =>
        {
            // Car Swerving
            trigger2.TriggerSubtitle(() =>
            {
                // Skip to next scene
                string targetPrompt = GetPrompt();
                prompt.text = (targetPrompt == null) ? "" : $"[E] {targetPrompt}";
                JacobsCarManager.state = JacobsCarState.Day3SkipToBackBay;
            });
        });
    }

    private void delayedGoingHomeSubtitlePlay()
    {
        trigger3.TriggerSubtitle(() =>
        {
            Invoke("part2SubtitleEnd", 6);
        });
    }

    private void part2SubtitleEnd()
    {
        trigger4.TriggerSubtitle(() =>
        {
            GameManager.cameraMove = false;
            day3Choice.PresentChoice((int choiceIndex) =>
            {
                if (choiceIndex == 0)
                {
                    GameManager.day3BranchRomanticRoute = false;
                    Debug.Log($"End Game: {choiceIndex}");
                }
                else if (choiceIndex == 1)
                {
                    GameManager.day3BranchRomanticRoute = true;
                    Debug.Log($"End Game: {choiceIndex}");
                }
                Invoke("FadeOut", 5);
                SceneManager.LoadScene("TomsRoom");
                GameManager.state = GameState.Day4StartTomsRoom;
            });
        });
    }

    public string GetPrompt() 
    { 
        return "Skip to BackBay"; 
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
