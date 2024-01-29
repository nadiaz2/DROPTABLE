using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomSelfDialogue : MonoBehaviour
{

    public SubtitleTrigger trigger;
    public SubtitleTrigger trigger2;

    private bool triggerStarted = false;
    private bool trigger2Started = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        switch(ClassroomManager.state)
        {
            case ClassroomState.Start:
                if (!triggerStarted)
                {
                    trigger.TriggerSubtitle();
                    triggerStarted = true;
                }
                break;

            case ClassroomState.ClassOver:
                if (!trigger2Started)
                {
                    trigger2.TriggerSubtitle();
                    trigger2Started = true;
                }
                break;

            case ClassroomState.Return:
                gameObject.SetActive(false);
                break;
        }
    }


}
