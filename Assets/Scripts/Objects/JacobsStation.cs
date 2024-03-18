using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JacobsStation : MonoBehaviour, Interactable
{

    public bool interactable = false;

    public void Interact()
    {
        this.interactable = false;
        //TODO Show computer UI Screen
        //TODO Show a enter password screen
        //TODO Play Mini game
        //TODO Rest of the computer stuff

        //Skip top part for now 
        GameManager.state = GameState.Day4FinishedMiniGame;
    }

    public string GetPrompt()
    {
        return "Jacob's Station";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}
