using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuessingInteractable : MonoBehaviour, Interactable
{
    public ClothingStoreManager clothingStoreManager;

    public bool hasInteracted = false;

    public void Interact()
    {
        this.hasInteracted = true;
        clothingStoreManager.itemCount++;
    }

    public string GetPrompt()
    {
        return "Ask about this item";
    }

    public bool IsActive()
    {
        return !(this.hasInteracted) && (clothingStoreManager.itemsActive);
    }
}
