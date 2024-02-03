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
        this.active = (TomsRoomManager.state == TomsRoomState.RachelDeathMessageSeen);
    }

    public void Interact()
    {
        switch (TomsRoomManager.state)
        {
            case TomsRoomState.Start:
                SceneManager.LoadScene("LivingRoom");
                break;

            case TomsRoomState.RachelDeathMessageSeen:
                GameManager.state = GameState.Day2HeadBackToSchool;
                SceneManager.LoadScene("LivingRoom");
                break;
        }

        switch (JacobsRoomManager.state)
        {
            case JacobsRoomState.Start:
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
