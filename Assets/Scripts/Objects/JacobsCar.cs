using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JacobsCar : MonoBehaviour, Interactable
{

    public bool interactable = false;

    public void Interact()
    {
        this.interactable = false;
    }

    public string GetPrompt()
    {
        return "Passenger Seat";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}
