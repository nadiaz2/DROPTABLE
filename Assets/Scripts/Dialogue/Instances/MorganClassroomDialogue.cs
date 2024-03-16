using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MorganClassroomDialogue : MonoBehaviour
{

    public DialogueTrigger trigger;
    public SubtitleTrigger Subtrigger;

    private bool triggerStarted = false;
    private bool talking;
    private bool subtitlestarted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (ClassroomManager.state) 
        {
            // Play subtitle after picking up headphones
            case ClassroomState.PickedUpHeadphones:
                if (!subtitlestarted)
                {
                    Subtrigger.TriggerSubtitle();
                    subtitlestarted = true;
                }
                break;

            // Play Morgan Dialogue
            case ClassroomState.AfterHeadphones:
                if (!triggerStarted)
                {
                    if (!talking)
                    {
                        trigger.TriggerDialogue(() =>
                        {
                            Invoke("delayedStateChange", 3);
                        });
                        triggerStarted = true;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    trigger.ContinueDialogue();
                }
                break;
        }

    }


    private void delayedStateChange()
    {
        ClassroomManager.state = ClassroomState.FinishedTalkMorgan;
    }
}
