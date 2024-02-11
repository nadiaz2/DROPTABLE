using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JacobsDoor : MonoBehaviour, Interactable
{
    public SubtitleTrigger subtitleTrigger;

    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false;

    }

    // Update is called once per frame
    void Update()
    {
        this.active = (LivingRoomManager.state == LivingRoomState.Day1ReturnHome || LivingRoomManager.state == LivingRoomState.Day1JacobsBackAfterBed || LivingRoomManager.state == LivingRoomState.Day2TalkedWithJacob);
    }

    public void Interact()
    {
        switch (LivingRoomManager.state)
        {
            case LivingRoomState.Day1ReturnHome:
                SceneManager.LoadScene("JacobsRoom");
                break;

            case LivingRoomState.Day1JacobsBackAfterBed:
                subtitleTrigger.TriggerSubtitle();
                break;

            case LivingRoomState.Day2TalkedWithJacob:
                subtitleTrigger.TriggerSubtitle();
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
