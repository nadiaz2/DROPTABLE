using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SchoolCorridorDoor : MonoBehaviour, Interactable
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
        this.active = (LabManager.state == LabState.Day4Run);
    }

    public void Interact()
    {
        switch (LabManager.state)
        {
            case LabState.Day4Run:
                SceneManager.LoadScene("SchoolCorridor");
                break;
        }
    }


    public string GetPrompt()
    {
        return "Run";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
