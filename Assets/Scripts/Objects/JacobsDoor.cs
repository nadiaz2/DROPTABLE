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
        active = true;

    }

    // Update is called once per frame
    void Update()
    {
        //this.active = (LivingRoomManager.state == LivingRoomState.Start);
    }

    public void Interact()
    {
        switch (LivingRoomManager.state)
        {
            case LivingRoomState.Start:
                GameManager.state = GameState.OnWayHomeStart;
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
