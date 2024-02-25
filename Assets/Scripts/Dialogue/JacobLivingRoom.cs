using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JacobLivingRoom : MonoBehaviour, Interactable
{
    public DialogueTrigger dialogueTrigger;
    public SubtitleTrigger foodArrivedSubtitle;
    public DialogueTrigger dialogueTriggerDay3;

    public SplineMovement getFoodPath;
    public LivingRoomPhone phone;

    public bool interactable = false;
    private bool dialogueTriggerStarted = false;
    private bool dialogueTriggerDay3Started = false;


    public void EndStudySession()
    {
        foodArrivedSubtitle.TriggerSubtitle(() => {
            getFoodPath.StartMovement();
            phone.active = true;
        });
    }

    public void Interact()
    {
        switch (LivingRoomManager.state)
        {
            case LivingRoomState.Day1JacobsBackAfterBed:
                if (!dialogueTriggerStarted)
                {
                    dialogueTrigger.TriggerDialogue(() => {
                        this.interactable = false;
                        LivingRoomManager.state = LivingRoomState.Day1TalkedWithJacob;

                        LivingRoomManager manager = LivingRoomManager.currentInstance;
                        manager.FadeOut();
                        manager.Invoke("SitCharacters", 2.5f);
                        Invoke("EndStudySession", 2.7f);
                        manager.Invoke("FadeIn", 3.0f);
                    });
                    dialogueTriggerStarted = true;
                }
                else
                {
                    dialogueTrigger.ContinueDialogue();
                }
                break;

            case LivingRoomState.Day3Start:

                break;
        }

        switch (GameManager.state)
        {
            case GameState.Day3FinishedMiniGame:
                if (!dialogueTriggerDay3Started)
                {
                    dialogueTriggerDay3.TriggerDialogue(() =>
                    {
                        this.interactable = false;
                        LivingRoomManager.state = LivingRoomState.Day3TalkedWithJacob;
                        GameManager.state = GameState.Day3TalkedWithJacob;
                    });
                    dialogueTriggerDay3Started = true;
                }
                else
                {
                    dialogueTriggerDay3.ContinueDialogue();
                }
                break;
        }
    }

    public string GetPrompt()
    {
        return "Talk to Jacob";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}
