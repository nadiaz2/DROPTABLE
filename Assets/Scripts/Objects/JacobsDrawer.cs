using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JacobsDrawer : MonoBehaviour, Interactable
{

    public bool interactable = false;

    public void Interact()
    {
        this.interactable = false;
        // Play the doorknob minigame again but for the drawer
        // If win then change state
        JacobsRoomManager.state = JacobsRoomState.Day4FoundKey;
    }

    public string GetPrompt()
    {
        return "Search";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}
