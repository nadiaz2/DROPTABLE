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
}

public class CafeteriaManager : MonoBehaviour
{
    public SubtitleTrigger trigger;
    public Lunch lunch;

    public static CafeteriaState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        if (GameManager.state == GameState.Day2HeadBackToSchool)
        {
            trigger.TriggerSubtitle();
            state = CafeteriaState.Day2LunchTime;
            lunch.interactable = true;
        }
    }
}
