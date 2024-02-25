using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneFoundDialogue : MonoBehaviour
{

    public DialogueTrigger trigger;

    public static bool photoFound = false;
    private bool hasTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photoFound && !hasTriggered) {
            trigger.TriggerDialogue();
            hasTriggered = true;
        }
    }
}
