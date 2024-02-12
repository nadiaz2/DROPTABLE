using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[System.Flags]
public enum OutsideHomeState
{
    Start,

    //Day 1
    BackFromSchool,

    //Day 2
    Day2BackFromSchool,

    //Day 3
    Day3HeadToBackBay,
}

public class OutsideHomeManager : MonoBehaviour
{
    public GameObject onWayHomeSceneTrigger;

    public TrashBags trashBags;
    public JacobsCar jacobsCar;

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


        if (GameManager.state == GameState.GameStart)
        {
            GameManager.state = GameState.Day3TalkedWithJacob;
        }


        switch (GameManager.state)
        {
            case GameState.ReturningHomeAfterHeadphones:
                OutsideHomeManager.state = OutsideHomeState.BackFromSchool;
                trashBags.interactable = true;
                break;

            case GameState.Day2HeadBackToSchool:
                onWayHomeSceneTrigger.SetActive(true);
                break;

            case GameState.Day2HeadBackHome:
                OutsideHomeManager.state = OutsideHomeState.Day2BackFromSchool;
                break;

            case GameState.Day3TalkedWithJacob:
                OutsideHomeManager.state = OutsideHomeState.Day3HeadToBackBay;
                jacobsCar.gameObject.SetActive(true);
                jacobsCar.interactable = true;
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