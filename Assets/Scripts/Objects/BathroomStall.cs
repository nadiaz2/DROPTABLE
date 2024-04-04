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


    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }
    

    public void Interact()
    {
        switch (GameManager.state)
        {
            case GameState.Day4Run:
                active = false;
                BathroomManager.state = BathroomState.Day4Phone;
                player.TeleportPlayer(insideStall);
                cameraChange();
                break;
        }
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
