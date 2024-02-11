using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JacobsCarState
{
    Start,
    PlayingGame,
    GamePaused,

    // Day 3 States
    Day3GoingToBackBay,

}

public class JacobsCarManager : MonoBehaviour
{
    public SubtitleTrigger trigger;
    public SubtitleTrigger trigger2;

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

    private void delayedSubtitlePlay()
    {
        trigger.TriggerSubtitle(() =>
        {
            // Car Swerving
            trigger2.TriggerSubtitle();
        });
    }
}
