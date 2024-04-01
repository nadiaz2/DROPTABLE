using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutsideDoor : MonoBehaviour, Interactable
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
        this.active = (LivingRoomManager.state == LivingRoomState.Day2Start || LivingRoomManager.state == LivingRoomState.Day3TalkedWithJacob || LivingRoomManager.state == LivingRoomState.Day4GoToLab);
    }

    public void Interact()
    {
        switch (LivingRoomManager.state)
        {
            case LivingRoomState.Day2Start:
                SceneManager.LoadScene("OutsideHome");
                break;

            case LivingRoomState.Day3TalkedWithJacob:
                SceneManager.LoadScene("OutsideHome");
                break;

            case LivingRoomState.Day4GoToLab:
                SceneManager.LoadScene("OutsideHome");
                break;

        }

    }


    public string GetPrompt()
    {
        return "Outside";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
