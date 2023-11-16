using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;
    public static bool dialogueStart = false;


    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>(gameObject).StartDialogue(dialogue);
        dialogueStart = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }



}
