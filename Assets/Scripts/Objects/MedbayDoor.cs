using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MedbayDoor : MonoBehaviour, Interactable
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
        this.active = (GameManager.state == GameState.Day4Run);
    }

    public void Interact()
    {
        switch (GameManager.state)
        {
            case GameState.Day4Run:
                SceneManager.LoadScene("Medbay");
                break;
        }
    }


    public string GetPrompt()
    {
        return "Medbay";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
