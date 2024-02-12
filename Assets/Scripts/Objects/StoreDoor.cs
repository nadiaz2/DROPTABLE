using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreDoor : MonoBehaviour, Interactable
{
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.active = (BackBayManager.state == BackBayState.Day3GoInsideStore);
    }

    public void Interact()
    {
        switch (BackBayManager.state)
        {
            case BackBayState.Day3GoInsideStore:
                SceneManager.LoadScene("ClothingStore");
                break;

        }

    }


    public string GetPrompt()
    {
        return "Head Inside";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
