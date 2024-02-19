using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClothingStoreYellowDress : MonoBehaviour, Interactable
{
    public DialogueTrigger dialogueTrigger;

    public bool interactable = false;

    private bool started = false;

    public void Interact()
    {
        //gameObject.SetActive(false);
        if (!started)
        {
            dialogueTrigger.TriggerDialogue(() =>
            {
                this.interactable = false;
            });
            started = false;
        }
        else
        {
            dialogueTrigger.ContinueDialogue();
        }
    }

    public string GetPrompt()
    {
        return "Look at the Dress";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}
