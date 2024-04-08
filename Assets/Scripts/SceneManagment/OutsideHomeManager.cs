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
    public PlayerMovement2D player;
    public GameObject onWayHomeSceneTrigger;
    public GameObject onWayHomeSceneTriggerDay4;

    public TrashBags trashBags;
    public JacobsCar jacobsCar;

    public BlackScreen blackScreen;

    [Header("Spawn Locations")]
    public Transform doorSpawn;

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


        switch (GameManager.state)
        {
            case GameState.ReturningHomeAfterHeadphones:
                OutsideHomeManager.state = OutsideHomeState.BackFromSchool;
                trashBags.interactable = true;
                break;

            case GameState.Day2HeadBackToSchool:
                onWayHomeSceneTrigger.SetActive(true);
                player.TeleportPlayer(doorSpawn);
                break;

            case GameState.Day2HeadBackHome:
                OutsideHomeManager.state = OutsideHomeState.Day2BackFromSchool;
                break;

            case GameState.Day3TalkedWithJacob:
                OutsideHomeManager.state = OutsideHomeState.Day3HeadToBackBay;
                jacobsCar.gameObject.SetActive(true);
                jacobsCar.interactable = true;
                player.TeleportPlayer(doorSpawn);
                break;

            case GameState.Day4HeadToJacobsLab:
                onWayHomeSceneTriggerDay4.SetActive(true);
                jacobsCar.gameObject.SetActive(true);
                player.TeleportPlayer(doorSpawn);
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