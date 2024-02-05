using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CafeteriaDoor : MonoBehaviour, Interactable
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
        this.active = (CafeteriaManager.state == CafeteriaState.Day2FinishedTalkingWithEmily);
    }

    public void Interact()
    {
        switch (CafeteriaManager.state)
        {
            case CafeteriaState.Day2FinishedTalkingWithEmily:
                GameManager.state = GameState.Day2AfternoonClass;
                SceneManager.LoadScene("Classroom");
                break;

       
        }
    }


    public string GetPrompt()
    {
        return "Head to Classroom";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
