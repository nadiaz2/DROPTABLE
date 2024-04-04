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
    Day4EndGameCallDirectly,
    Day4EndGamePutInPocket,
    Day4EndGameDontCall,

}

public class BathroomManager : MonoBehaviour
{

    public SubtitleTrigger checkPhoneTrigger;
    public ChoiceTrigger bathroomEndingTwoChoice;
    public ChoiceTrigger bathroomEndingThreeChoice;

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
                    //TODO Give choices for player to choose if they want to call morgan or not, also shows on phone side
                    checkPhoneTrigger.TriggerSubtitle(() =>
                    {
                        //TODO Switch to first person

                        if (GameManager.day3BranchRomanticRoute == true)
                        {
                            bathroomEndingThreeChoice.PresentChoice((int choiceIndex) => {
                                if (choiceIndex == 0)
                                {
                                    BathroomManager.state = BathroomState.Day4EndGameCallDirectly;
                                    Debug.Log($"Day 4 End Game: {choiceIndex}");
                                    return;
                                }
                                else if (choiceIndex == 1)
                                {
                                    BathroomManager.state = BathroomState.Day4EndGamePutInPocket;
                                    Debug.Log($"Day 4 End Game: {choiceIndex}");
                                    return;
                                }
                                else if (choiceIndex == 2) // Only happens when going through romantic route
                                {
                                    BathroomManager.state = BathroomState.Day4EndGameDontCall;
                                    Debug.Log($"Day 4 End Game: {choiceIndex}");
                                    return;
                                }
                            });
                        }
                        else if (GameManager.day3BranchRomanticRoute == false)
                        {
                            bathroomEndingTwoChoice.PresentChoice((int choiceIndex) => {
                                if (choiceIndex == 0)
                                {
                                    BathroomManager.state = BathroomState.Day4EndGameCallDirectly;
                                    Debug.Log($"Day 4 End Game: {choiceIndex}");
                                    return;
                                }
                                else if (choiceIndex == 1)
                                {
                                    BathroomManager.state = BathroomState.Day4EndGamePutInPocket;
                                    Debug.Log($"Day 4 End Game: {choiceIndex}");
                                    return;
                                }
                            });
                        }
                        
                    });
                    checkPhoneTriggered = true;
                }
                break;

            //TODO Choice 1: Call will give bad end and jacob will show up
            //TODO Camera will be first person
            case BathroomState.Day4EndGameCallDirectly:
                SceneManager.LoadScene("TomsRoom");
                break;

            //TODO Choice 2: Call but put in pocket
            //TODO Play Dialogue
            //TODO Bring up another branch for player to choose
            case BathroomState.Day4EndGamePutInPocket:

                break;

            //TODO Choice 3: Don't Call (Flip phone over)
            //TODO This route only appears if the romatic route was chosen
            case BathroomState.Day4EndGameDontCall:

                break;

            //TODO Choice 1: Don't Hang up
            //TODO Jacob gets shot by morgan
            //TODO police car scene

            //TODO Choice 2: Hang up
            //TODO Both Jacob and Tom get shot by Morgan
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
