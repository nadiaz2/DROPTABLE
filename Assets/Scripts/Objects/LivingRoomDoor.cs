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
        active = true;

    }

    // Update is called once per frame
    void Update()
    {
        //this.active = (LivingRoomManager.state == LivingRoomState.Start);
    }

    public void Interact()
    {
        switch (TomsRoomManager.state)
        {
            case TomsRoomState.Start:
                GameManager.state = GameState.OnWayHomeStart;
                SceneManager.LoadScene("LivingRoom");
                break;
        }

        switch (JacobsRoomManager.state)
        {
            case JacobsRoomState.Start:
                GameManager.state = GameState.OnWayHomeStart;
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
