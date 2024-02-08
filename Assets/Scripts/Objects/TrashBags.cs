using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashBags : MonoBehaviour, Interactable
{
    public SubtitleTrigger trigger;

    public bool interactable = false;

    public void Interact()
    {
        this.interactable = false;

        trigger.TriggerSubtitle();
    }

    public string GetPrompt()
    {
        return "Trash Bags";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}
