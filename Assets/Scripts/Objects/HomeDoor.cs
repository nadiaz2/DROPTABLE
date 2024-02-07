using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeDoor : MonoBehaviour, Interactable
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
        this.active = (OutsideHomeManager.state == OutsideHomeState.Day2BackFromSchool);
    }

    public void Interact()
    {
        switch (OutsideHomeManager.state)
        {
            case OutsideHomeState.Day2BackFromSchool:
                SceneManager.LoadScene("LivingRoom");
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
