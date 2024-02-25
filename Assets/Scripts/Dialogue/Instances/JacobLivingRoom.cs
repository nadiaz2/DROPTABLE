using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JacobLivingRoom : MonoBehaviour, Interactable
{
    public LivingRoomPhone phone;

    [Header("Day 1")]
    public DialogueTrigger jacobReturnDialogue;
    public SubtitleTrigger foodArrivedSubtitle;
    public SplineMovement getFoodPath;
    public SplineMovement returnFromFoodPath;
    public DialogueTrigger backFromFoodDialogue;
    public ChoiceTrigger day1Choice;


    [Header("Day 3")]
    public DialogueTrigger dialogueTriggerDay3;


    [HideInInspector]
    public bool interactable = false;
    private bool dialogueTriggerStarted = false;
    private bool dialogueTriggerDay3Started = false;

    private LivingRoomState lastState;

    public void Start() {
        lastState = LivingRoomManager.state;
    }

    public void Update() {
        if(LivingRoomManager.state != lastState) {
            lastState = LivingRoomManager.state;
            if(LivingRoomManager.state == LivingRoomState.Day1FoundPhoto) {
                returnFromFoodPath.StartMovement();
                interactable = true;
                backFromFoodDialogue.TriggerDialogue(() => {
                    interactable = false;
                    day1Choice.PresentChoice((int choiceIndex) => {
                        Debug.Log(choiceIndex);
                    });
                });
            }
        }
    }

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
                    jacobReturnDialogue.TriggerDialogue(() => {
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
                    jacobReturnDialogue.ContinueDialogue();
                }
                break;

            case LivingRoomState.Day1FoundPhoto:
                backFromFoodDialogue.ContinueDialogue();
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
