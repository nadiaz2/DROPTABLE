using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TomsDoor : MonoBehaviour, Interactable
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
                SceneManager.LoadScene("TomsRoom");
                break;

        }
    }


    public string GetPrompt()
    {
        return "Tom's Room";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
