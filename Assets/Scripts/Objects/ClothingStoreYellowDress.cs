using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClothingStoreYellowDress : MonoBehaviour, Interactable
{
    public DialogueTrigger dialogueTrigger;

    public bool interactable = false;

    private bool started = false;

    void Update()
    {
        switch (ClothingStoreManager.state)
        {
            case ClothingStoreState.Day3StartYellowDressDialogue:
                if (!started)
                {
                    dialogueTrigger.TriggerDialogue(() =>
                    {
                        ClothingStoreManager.state = ClothingStoreState.Day3EmilyFlashBack;
                    });
                    started = true;
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        dialogueTrigger.ContinueDialogue();
                    }
                }
                break;
        }
    }

    public void Interact()
    {
        //gameObject.SetActive(false);
        ClothingStoreManager.state = ClothingStoreState.Day3StartYellowDressDialogue;
        this.interactable = false;
    }

    public string GetPrompt()
    {
        return "Look at the Dress";
    }

    public bool IsActive()
    {
        return this.interactable;
    }
}
