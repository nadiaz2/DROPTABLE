using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JacobsDoor : MonoBehaviour, Interactable
{
    public SubtitleTrigger subtitleTriggerDay1;
    public SubtitleTrigger subtitleTriggerDay3;
    public SubtitleTrigger subtitleTriggerDay4;

    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false;

    }

    // Update is called once per frame
    void Update()
    {
        this.active = (LivingRoomManager.state == LivingRoomState.Day1ReturnHome || LivingRoomManager.state == LivingRoomState.Day1JacobsBackAfterBed || 
                        LivingRoomManager.state == LivingRoomState.Day1TalkedWithJacob || LivingRoomManager.state == LivingRoomState.Day3Start ||
                        LivingRoomManager.state == LivingRoomState.Day4Start);
    }

    public void Interact()
    {
        switch (LivingRoomManager.state)
        {
            case LivingRoomState.Day1ReturnHome:
                SceneManager.LoadScene("JacobsRoom");
                break;

            case LivingRoomState.Day1JacobsBackAfterBed:
                subtitleTriggerDay1.TriggerSubtitle();
                break;

            case LivingRoomState.Day1TalkedWithJacob:
                subtitleTriggerDay1.TriggerSubtitle();
                break;

            case LivingRoomState.Day3Start:
                LivingRoomManager.state = LivingRoomState.Day3StartMinigame;
                subtitleTriggerDay3.TriggerSubtitle(() =>
                {
                    //TODO Play Minigame

                    GameManager.state = GameState.Day3FinishedMiniGame;
                });
                break;

            case LivingRoomState.Day4Start:
                //TODO play knocking sound effects
                LivingRoomManager.state = LivingRoomState.Day4StartMinigame;
                subtitleTriggerDay4.TriggerSubtitle(()=>
                {
                    // Play the Doorknob mimi game again
                    // If win go inside jacobs room
                    SceneManager.LoadScene("JacobsRoom");
                });
                break;
        }
    }


    public string GetPrompt()
    {
        return "Jacob's Room";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
