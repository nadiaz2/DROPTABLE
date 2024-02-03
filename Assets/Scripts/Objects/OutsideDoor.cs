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
        this.active = (LivingRoomManager.state == LivingRoomState.Day2Start);
    }

    public void Interact()
    {
        switch (LivingRoomManager.state)
        {
            case LivingRoomState.Day2Start:
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
