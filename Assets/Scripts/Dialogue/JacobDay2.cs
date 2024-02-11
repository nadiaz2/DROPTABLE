using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JacobDay2 : MonoBehaviour, Interactable
{
    public DialogueTrigger dialogueTrigger;

    public bool interactable = false;
    private bool dialogueTriggerStarted = false;


    // Update is called once per frame
    void Update()
    {
        switch (LivingRoomManager.state)
        {
            case LivingRoomState.Day1JacobsBackAfterBed:
                break;
        }
    }

    public void Interact()
    {
        if (!dialogueTriggerStarted)
        {
            dialogueTrigger.TriggerDialogue(() => { 
                this.interactable = false; 
            });
            dialogueTriggerStarted = true;
        }
        else
        {
            dialogueTrigger.ContinueDialogue();
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
