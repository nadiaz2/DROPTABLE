using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmilyCafeteria : MonoBehaviour, Interactable
{
    public DialogueManager dialogueManager;
    public SubtitleManager subtitleManager;
    public SubtitleTrigger subtitleTrigger;
    public SubtitleTrigger subtitleTrigger2;
    public DialogueTrigger dialogueTrigger;
    public GameObject rachelPhoto;
    public GameObject player;

    public bool interactable = false;
    private bool subtitleTriggerStarted = false;
    private bool subtitleTriggerStarted2 = false;
    private bool dialogueTriggerStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (CafeteriaManager.state)
        {
            case CafeteriaState.Day2EmilyPhone:
                if (!subtitleTriggerStarted)
                {
                    subtitleTrigger.TriggerSubtitle();
                    interactable = true;
                    subtitleTriggerStarted = true;
                }
                break;

            case CafeteriaState.Day2SeenPhoto:
                if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                {
                    rachelPhoto.SetActive(false);
                    player.gameObject.SetActive(true);
                    CafeteriaManager.state = CafeteriaState.Day2TalkWithEmily;
                }
                break;

            case CafeteriaState.Day2TalkWithEmily:
                if (!dialogueTriggerStarted)
                {
                    dialogueTrigger.TriggerDialogue();
                    dialogueTriggerStarted = true;
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        dialogueTrigger.ContinueDialogue();
                    }
                }
                if(dialogueManager.finished && !subtitleTriggerStarted2)
                {
                    subtitleTrigger2.TriggerSubtitle();
                    subtitleTriggerStarted2 = true;
                }
                break;
        }
    }

    public void Interact()
    {
        this.interactable = false;
        rachelPhoto.SetActive(true);
        player.gameObject.SetActive(false);
        subtitleManager.EndDialogue();
        CafeteriaManager.state = CafeteriaState.Day2SeenPhoto;

    }

    public string GetPrompt()
    {
        return "Look at Photo";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}
