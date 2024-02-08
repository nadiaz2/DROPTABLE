using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TomsBed : MonoBehaviour, Interactable
{
    //public SubtitleTrigger trigger;

    public bool interactable = false;

    public void Interact()
    {
        this.interactable = false;
        Debug.Log("Slept");
        //trigger.TriggerSubtitle();
    }

    public string GetPrompt()
    {
        return "Lay Down";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}
