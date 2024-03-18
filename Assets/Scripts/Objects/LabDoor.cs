using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LabDoor : MonoBehaviour, Interactable
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
        this.active = (SchoolCorridorManager.state == SchoolCorridorState.Day4CheckJacobsLab || SchoolCorridorManager.state ==  SchoolCorridorState.Day4LabDoorsOpen);
    }

    public void Interact()
    {
        switch (SchoolCorridorManager.state)
        {
            case SchoolCorridorState.Day4CheckJacobsLab:
                SchoolCorridorManager.state = SchoolCorridorState.Day4TryToOpenDoor;
                break;

            case SchoolCorridorState.Day4LabDoorsOpen:
                SceneManager.LoadScene("Lab");
                GameManager.state = GameState.Day4InLab;
                break;
        }
    }


    public string GetPrompt()
    {
        return "Jacob's Lab";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
