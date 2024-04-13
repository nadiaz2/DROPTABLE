using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JacobsDrawer : MonoBehaviour, Interactable
{
    public SubtitleTrigger day4StealingKeyTrigger;
    public DoorMinigameController drawerMinigame;
    public SubtitleTrigger day4JacobFlashbackTrigger;

    public bool interactable = false;

    public void Interact()
    {
        this.interactable = false;

        if (JacobsRoomManager.state == JacobsRoomState.Day4SearchJacobsRoom)
        {
            // Play the doorknob minigame again but for the drawer
            // If win then change state
            day4StealingKeyTrigger.TriggerSubtitle(() =>
            {
                drawerMinigame.StartMinigame((result) =>
                {
                    Debug.Log(result);
                    if (result)
                    {
                        JacobsRoomManager.state = JacobsRoomState.Day4FoundKey;
                    }
                });
            });
        }
        else if (JacobsRoomManager.state == JacobsRoomState.Day4FlashBack)
        {
            JacobsRoomManager.state = JacobsRoomState.Day4FlashBackRoomDone;
            day4JacobFlashbackTrigger.TriggerSubtitle(() =>
            {
                SchoolCorridorManager.state = SchoolCorridorState.Day4FlashBack;
                SceneManager.LoadScene("SchoolCorridor");
            });
        }

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
