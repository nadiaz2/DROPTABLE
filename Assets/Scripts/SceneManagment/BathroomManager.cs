using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.SceneManagement;

public enum BathroomState
{

    // Day 4
    Day4Start,
    Day4Phone,

}

public class BathroomManager : MonoBehaviour
{

    public SubtitleTrigger checkPhoneTrigger;

    private bool checkPhoneTriggered;

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
        switch (BathroomManager.state)
        {
            case BathroomState.Day4Start:
                break;

            case BathroomState.Day4Phone:
                if (!checkPhoneTriggered)
                {
                    //Send message to phone and tell player to call morgan
                    checkPhoneTrigger.TriggerSubtitle(() =>
                    {
                        //TODO Switch to first person
                    });
                    checkPhoneTriggered = true;
                }

                //TODO Give choices for player to choose if they want to call morgan or not, also shows on phone side

                //TODO Choice 1: Call will give bad end and jacob will show up
                //TODO Camera will be first person

                //TODO Choice 3: Don't Call (Flip phone over)
                //TODO This route only appears if the romatic route was chosen

                //TODO Choice 2: Call but put in pocket
                //TODO Play Dialogue
                //TODO Bring up another branch for player to choose

                //TODO Choice 1: Don't Hang up
                //TODO Jacob gets shot by morgan
                //TODO police car scene

                //TODO Choice 2: Hang up
                //TODO Both Jacob and Tom get shot by Morgan
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
