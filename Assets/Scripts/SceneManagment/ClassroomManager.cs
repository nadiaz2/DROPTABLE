using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[System.Flags]
public enum ClassroomState
{
    // States for first time in classroom
    Start = 1,
    Seated = 2,
    ClassOver = 4,

    // States for returning to pick up headphones
    Return = 8,
    PickedUpHeadphones = 16,
    MorganCloseUp = 32,
    AfterHeadphones = 64,

    // States for Day 2
    Day2AfternoonClass,
    Day2ClassOver,
    Day2Seated,
}

public class ClassroomManager : MonoBehaviour
{
    public GameObject jacob;
    public Headphones headphones;

    public SubtitleTrigger subtitleTriggerDay2;
    public bool day2SubtitleTriggered;

    public BlackScreen blackScreen;

    public static ClassroomState state { get; set; }
    public static ClassroomManager currentInstance
    {
        get
        {
            return _instance;
        }
    }
    private static ClassroomManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        ClassroomManager._instance = this;

        blackScreen.goBlacked = false;

        if (GameManager.state == GameState.GameStart)
        {
            //GameManager.state = GameState.BackToClassroom;
        }


        // Returning to classroom to pick up headphones
        if (GameManager.state == GameState.BackToClassroom)
        {
            jacob.SetActive(false);
            headphones.gameObject.SetActive(true);
            headphones.interactable = true;
            state = ClassroomState.Return;
        }else if (GameManager.state == GameState.Day2AfternoonClass)
        {
            jacob.SetActive(false);
            day2SubtitleTriggered = false;
            state = ClassroomState.Day2AfternoonClass;
        }
        else
        {
            // Start of the game
            state = ClassroomState.Start;
        }

    }

    private void Update()
    {
        switch (ClassroomManager.state)
        {
            case ClassroomState.Day2Seated:
                if (!day2SubtitleTriggered)
                {
                    subtitleTriggerDay2.TriggerSubtitle(() =>
                    {
                        ClassroomManager.state = ClassroomState.Day2ClassOver;
                    });
                    day2SubtitleTriggered = true;
                }

                break;
        }
    }

    public void FadeOut()
    {
        if(blackScreen != null)
        {
            blackScreen.goBlacked = true;
        }
    }

    public void FadeIn()
    {
        if(blackScreen != null)
        {
            blackScreen.goBlacked = false;
        }
    }

}
