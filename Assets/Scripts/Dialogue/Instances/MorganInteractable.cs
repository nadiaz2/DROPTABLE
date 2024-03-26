using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MorganInteractable : MonoBehaviour, Interactable
{

    public bool interactable = false;

    private bool interacted = false;


    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Day4JacobFlashBack)
        {
            if (!interacted) 
            {
                interacted = true;
                this.interactable = true;
            }
            
        }
    }

    public void Interact()
    {
        this.interactable = false;
        SchoolCorridorManager.state = SchoolCorridorState.Day4FlashBackTalkToMorgan;
    }

    public string GetPrompt()
    {
        return "Officer Morgan";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}
