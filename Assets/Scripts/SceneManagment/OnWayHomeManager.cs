using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum OnWayHomeState
{
    Start = 1
}

public class OnWayHomeManager : MonoBehaviour
{
    public GameObject classroomSceneTrigger;
    public GameObject outsideHomeSceneTriggerDay1;
    public GameObject cafeteriaSceneTrigger;
    public GameObject outsideHomeSceneTriggerDay2;
    public GameObject schoolCorridorSceneTrigger;
    public SubtitleTrigger subtitleTrigger;

    public PlayerMovement2D player;
    public Transform comingFromOutsideHouse;
    public Transform comingFromClassroom;

    public BlackScreen blackScreen;

    public static OnWayHomeState state { get; set; }
    public static OnWayHomeManager currentInstance
    {
        get
        {
            return _instance;
        }
    }
    private static OnWayHomeManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        OnWayHomeManager._instance = this;

        blackScreen.goBlacked = false;

        Debug.Log(GameManager.state);

        switch (GameManager.state)
        {
            case GameState.OnWayHomeStart:
                state = OnWayHomeState.Start;
                outsideHomeSceneTriggerDay1.SetActive(false);
                classroomSceneTrigger.SetActive(false);
                player.TeleportPlayer(comingFromClassroom);

                subtitleTrigger.TriggerSubtitle(() =>
                {
                    classroomSceneTrigger.SetActive(true);
                });
                break;

            case GameState.ReturningHomeAfterHeadphones:
                outsideHomeSceneTriggerDay1.SetActive(true);
                classroomSceneTrigger.SetActive(false);
                player.TeleportPlayer(comingFromClassroom);
                break;

            case GameState.Day2HeadBackToSchool:
                //Debug.Log(GameManager.state);
                cafeteriaSceneTrigger.SetActive(true);
                player.TeleportPlayer(comingFromOutsideHouse);
                break;

            case GameState.Day2HeadBackHome:
                outsideHomeSceneTriggerDay2.SetActive(true);
                player.TeleportPlayer(comingFromClassroom);
                break;

            case GameState.Day4HeadToJacobsLab:
                schoolCorridorSceneTrigger.SetActive(true);
                player.TeleportPlayer(comingFromOutsideHouse);
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