using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lunch : MonoBehaviour, Interactable
{
    public bool interactable = false;

    public void Interact()
    {
        this.interactable = false;
        //gameObject.SetActive(false);
        CafeteriaManager.state = CafeteriaState.Day2EmilyPhone;
    }

    public string GetPrompt()
    {
        return "Pick Up Lunch";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}
