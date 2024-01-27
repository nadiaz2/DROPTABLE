using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Dialogue[] dialogues;

    public void TriggerDialogue(Action callback = null)
    {
        dialogueManager.StartDialogue(dialogues, callback);
    }
}
