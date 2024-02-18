using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CafeteriaState
{
    Start,
    PlayingGame,
    GamePaused,

    // Day 2 States
    Day2LunchTime,
    Day2EmilyPhone,
    Day2SeenPhoto,
    Day2TalkWithEmily,
    Day2FinishedTalkingWithEmily,

}

public class CafeteriaManager : MonoBehaviour
{
    public SubtitleTrigger trigger;
    public Lunch lunch;

    public static CafeteriaState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // Testing
        if (GameManager.state == GameState.GameStart)
        {
            GameManager.state = GameState.Day2HeadBackToSchool;
        }

        if (GameManager.state == GameState.Day2HeadBackToSchool)
        {
            trigger.TriggerSubtitle();
            state = CafeteriaState.Day2LunchTime;
            lunch.interactable = true;
        }
    }
}
