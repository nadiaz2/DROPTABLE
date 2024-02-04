using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[System.Flags]
public enum OnWayHomeState
{
    Start = 1
}

public class OnWayHomeManager : MonoBehaviour
{
    public GameObject classroomSceneTrigger;
    public GameObject livingRoomSceneTrigger;
    public SubtitleTrigger subtitleTrigger;

    public GameObject player;
    public Transform comingFromOutsideHouse;

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

        if (GameManager.state == GameState.OnWayHomeStart)
        {
            state = OnWayHomeState.Start;
            livingRoomSceneTrigger.SetActive(false);
            classroomSceneTrigger.SetActive(false);

            subtitleTrigger.TriggerSubtitle(() =>
            {
                classroomSceneTrigger.SetActive(true);
            });
        }else if (GameManager.state == GameState.ReturningHomeAfterHeadphones)
        {
           livingRoomSceneTrigger.SetActive(true);
           classroomSceneTrigger.SetActive(false);

        }else if (GameManager.state == GameState.Day2HeadBackToSchool)
        {
            player.transform.position = comingFromOutsideHouse.position;
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