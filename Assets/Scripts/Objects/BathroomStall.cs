using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BathroomStall : MonoBehaviour, Interactable
{
    public PlayerMovement player;
    public Transform insideStall;

    private bool active;

    private bool inStall = false;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inStall)
        {
            this.active = (GameManager.state == GameState.Day4Run);
            inStall = true;
        }
    }

    public void Interact()
    {
        switch (GameManager.state)
        {
            case GameState.Day4Run:
                player.transform.position = insideStall.position;
                player.transform.rotation = insideStall.rotation;

                //TODO Switch to first person

                //TODO Send message to phone and tell player to call morgan
                //TODO Give choices for player to choose if they want to call morgan or not, also shows on phone side

                //TODO Choice 1: Call will give bad end and jacob will show up
                //TODO Camera will be first person

                //TODO Choice 3: Don't Call (Flip phone over)
                //TODO This route only appears if the romatic route was chosen

                //TODO Choice 2: Call but put in pocket
                //TODO Play Dialogue
                //TODO Bring up another branch for player to choose

                //TODO Choice 1: Don't Hang up
                //TODO Jacob gets shot by morgan
                //TODO police car scene

                //TODO Choice 2: Hang up
                //TODO Both Jacob and Tom get shot by Morgan
                break;
        }
    }


    public string GetPrompt()
    {
        return "Hide";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
