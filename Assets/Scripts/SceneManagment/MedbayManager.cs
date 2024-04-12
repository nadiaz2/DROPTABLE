using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MedbayState
{

    // Day 4
    Day4CheckJacobsLab,

}

public class MedbayManager : MonoBehaviour
{

    public BlackScreen blackScreen;

    public static MedbayState state { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        blackScreen.goBlacked = false;


        switch (GameManager.state)
        {
            case GameState.Day4Run:
                //TODO Create hiding spot and good/bad ending
                //TODO Pick up scalpel
                //TODO Killing Tom
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
