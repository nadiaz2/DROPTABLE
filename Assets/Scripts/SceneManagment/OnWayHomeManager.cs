using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[System.Flags]
public enum OnWayHomeState
{
    // States for first time in classroom
    Start = 1,
    Seated = 2,
    Return = 3,

}

public class OnWayHomeManager : MonoBehaviour
{
    public GameObject jacob;
    public Headphones headphones;

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

        // Returning to classroom to pick up headphones
        if (GameManager.state == GameState.BackToClassroom)
        {
            jacob.SetActive(false);
            headphones.gameObject.SetActive(true);
            headphones.interactable = true;
            state = OnWayHomeState.Return;
        }
        else
        {
            state = OnWayHomeState.Start;
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