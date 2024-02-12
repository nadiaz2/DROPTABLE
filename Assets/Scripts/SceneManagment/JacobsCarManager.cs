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

}

public class JacobsCarManager : MonoBehaviour
{
    public SubtitleTrigger trigger;
    public SubtitleTrigger trigger2;

    public Text prompt;

    public static JacobsCarState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        if (GameManager.state == GameState.GameStart)
        {
            GameManager.state = GameState.Day3TalkedWithJacob;
        }

        if (GameManager.state == GameState.Day3TalkedWithJacob)
        {
            state = JacobsCarState.Day3GoingToBackBay;
            Invoke("delayedSubtitlePlay", 5);
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

    public string GetPrompt() 
    { 
        return "Skip to BackBay"; 
    }
}
