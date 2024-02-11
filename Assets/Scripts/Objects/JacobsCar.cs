using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JacobsCar : MonoBehaviour, Interactable
{

    public bool interactable = false;

    public SubtitleTrigger trigger;

    public void Interact()
    {

        switch(OutsideHomeManager.state)
        {
            case OutsideHomeState.Day3HeadToBackBay:
                trigger.TriggerSubtitle(() =>
                {
                    SceneManager.LoadScene("JacobsCar");
                });
                this.interactable = false;
                break;
        }
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
