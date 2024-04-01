using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BathroomStall : MonoBehaviour, Interactable
{
    public PlayerMovement player;
    public Transform insideStall;
    public Camera mainCamera;
    public Transform oppositeCameraPosition;

    private bool changedCamera;

    private bool active;

    private bool inStall = false;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!inStall)
        {
            this.active = (GameManager.state == GameState.Day4Run);
        }
        else
        {
            this.active = false;
        }
    }

    public void Interact()
    {
        switch (GameManager.state)
        {
            case GameState.Day4Run:
                positionChange();
                inStall = true;
                BathroomManager.state = BathroomState.Day4Phone;
                cameraChange();
                break;
        }
    }

    private void positionChange()
    {
        player.transform.position = insideStall.position;
        player.transform.rotation = insideStall.rotation;
    }

    private void cameraChange()
    {
        if (!changedCamera)
        {
            if (BathroomManager.state == BathroomState.Day4Phone)
            {
                mainCamera.transform.SetPositionAndRotation(oppositeCameraPosition.position, oppositeCameraPosition.rotation);
            }
            changedCamera = true;
        }
    }


    public string GetPrompt()
    {
        return "Hide";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
