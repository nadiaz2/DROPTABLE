using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.SceneManagement;

public enum BathroomState
{

    // Day 4
    Day4CheckJacobsLab,

}

public class BathroomManager : MonoBehaviour
{

    public BlackScreen blackScreen;

    public static BathroomState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        blackScreen.goBlacked = false;


        switch (GameManager.state)
        {
            case GameState.Day4Run:

                break;
        }

    }

    private void Update()
    {

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
