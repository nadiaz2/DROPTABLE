using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Headphones : MonoBehaviour, Interactable
{
    public bool interactable = false;

    public void Interact()
    {
        this.interactable = false;
        gameObject.SetActive(false);
        ClassroomManager.state = ClassroomState.PickedUpHeadphones;
    }

    public string GetPrompt()
    {
        return "Pick Up Headphones";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}
