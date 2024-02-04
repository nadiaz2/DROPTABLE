using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[System.Flags]
public enum OutsideHomeState
{
    Start = 1
}

public class OutsideHomeManager : MonoBehaviour
{
    public GameObject onWayHomeSceneTrigger;
    public SubtitleTrigger subtitleTrigger;

    public BlackScreen blackScreen;

    public static OutsideHomeState state { get; set; }
    public static OutsideHomeManager currentInstance
    {
        get
        {
            return _instance;
        }
    }
    private static OutsideHomeManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        OutsideHomeManager._instance = this;

        blackScreen.goBlacked = false;


        if (GameManager.state == GameState.Day2HeadBackToSchool)
        {
            onWayHomeSceneTrigger.SetActive(true);
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