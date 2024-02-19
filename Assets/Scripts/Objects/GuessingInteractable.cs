using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuessingInteractable : MonoBehaviour, Interactable
{
    public ClothingStoreManager clothingStoreManager;

    public bool hasInteracted = false;

    private bool started = false;

    void Update()
    {
        if (!started)
        {
            this.hasInteracted = true;
        }
        if (ClothingStoreManager.state == ClothingStoreState.Day3InsideClothingStore && !started)
        {
            started = true;
            this.hasInteracted = false;
        }
    }

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
