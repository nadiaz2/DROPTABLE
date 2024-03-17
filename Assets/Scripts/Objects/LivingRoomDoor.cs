using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivingRoomDoor : MonoBehaviour, Interactable
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
        this.active = (TomsRoomManager.state == TomsRoomState.Day1Start || JacobsRoomManager.state == JacobsRoomState.Day1Start ||
                        TomsRoomManager.state == TomsRoomState.Day1JacobsBack || TomsRoomManager.state == TomsRoomState.RachelDeathMessageSeen ||
                        TomsRoomManager.state == TomsRoomState.StartDay3 || TomsRoomManager.state == TomsRoomState.Day4GoToJacobsRoom ||
                        JacobsRoomManager.state == JacobsRoomState.Day4FoundKey);
    }

    public void Interact()
    {
        switch (TomsRoomManager.state)
        {
            case TomsRoomState.Day1Start:
                SceneManager.LoadScene("LivingRoom");
                break;

            case TomsRoomState.Day1JacobsBack:
                GameManager.state = GameState.Day1JacobsBack;
                SceneManager.LoadScene("LivingRoom");
                break;

            case TomsRoomState.RachelDeathMessageSeen:
                GameManager.state = GameState.Day2HeadBackToSchool;
                SceneManager.LoadScene("LivingRoom");
                break;

            case TomsRoomState.StartDay3:
                SceneManager.LoadScene("LivingRoom");
                break;

            case TomsRoomState.Day4GoToJacobsRoom:
                SceneManager.LoadScene("LivingRoom");
                break;


        }

        switch (JacobsRoomManager.state)
        {
            case JacobsRoomState.Day1Start:
                SceneManager.LoadScene("LivingRoom");
                break;

            case JacobsRoomState.Day4FoundKey:
                SceneManager.LoadScene("LivingRoom");
                break;
        }

    }


    public string GetPrompt()
    {
        return "Living Room";
    }

    public bool IsActive()
    {
        return this.active;
    }

}
