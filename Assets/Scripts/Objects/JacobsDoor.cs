using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JacobsDoor : MonoBehaviour, Interactable
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
        this.active = (LivingRoomManager.state == LivingRoomState.Day1ReturnHome);
    }

    public void Interact()
    {
        switch (LivingRoomManager.state)
        {
            case LivingRoomState.Day1ReturnHome:
                SceneManager.LoadScene("JacobsRoom");
                break;
        }
    }


    public string GetPrompt()
    {
        return "Jacob's Room";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
