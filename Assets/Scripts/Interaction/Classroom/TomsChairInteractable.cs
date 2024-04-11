using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomsChairInteractable : MonoBehaviour, Interactable
{
    public PlayerMovement player;
    public Transform sittingPlacement;
    public Transform standingPlacement;
    public GameObject ChairSeat;

    private ClassroomState lastState;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        lastState = ClassroomManager.state;
        this.active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ClassroomManager.state == ClassroomState.Seated)
        {
            player.TeleportPlayer(sittingPlacement);
            //ChairSeat.transform.Rotate(0, 0, 88);
        }

        if (ClassroomManager.state == lastState)
        {
            return;
        }

        switch (ClassroomManager.state)
        {
            case ClassroomState.Start:
            case ClassroomState.ClassOver:
                this.active = true;
                break;
            case ClassroomState.Day2AfternoonClass:
                this.active = true;
                break;
            case ClassroomState.Day2ClassOver:
                this.active = true;
                break;
            default:
                this.active = false;
                break;
        }
        Debug.Log($"State Change {ClassroomManager.state}, Active {this.active}");

        lastState = ClassroomManager.state;
    }

    private IEnumerator ChangeSceneState(float waitTime, ClassroomState state)
    {
        yield return new WaitForSeconds(waitTime);
        ClassroomManager.state = state;
    }

    public void Interact()
    {
        switch (ClassroomManager.state)
        {
            case ClassroomState.Start:
                player.seated = true;
                player.immobile = true;
                player.TeleportPlayer(sittingPlacement);
                this.active = false;
                ClassroomManager.state = ClassroomState.Seated;
                break;

            case ClassroomState.ClassOver:
                player.seated = false;
                player.immobile = false;
                player.TeleportPlayer(standingPlacement);
                this.active = false;
                break;

            case ClassroomState.Day2AfternoonClass:
                player.seated = true;
                player.immobile = true;
                player.TeleportPlayer(sittingPlacement);
                this.active = false;
                ClassroomManager.currentInstance.Invoke("FadeOut", 1);
                StartCoroutine(ChangeSceneState(4f, ClassroomState.Day2Seated));
                ClassroomManager.currentInstance.Invoke("FadeIn", 4);
                break;

            case ClassroomState.Day2ClassOver:
                player.seated = false;
                player.immobile = false;
                player.TeleportPlayer(standingPlacement);
                this.active = false;
                break;

            default:
                Debug.Log($"Invalid Interaction State: {ClassroomManager.state}");
                break;
        }
    }

    public string GetPrompt()
    {
        if (ClassroomManager.state == ClassroomState.ClassOver || ClassroomManager.state == ClassroomState.Day2ClassOver)
        {
            return "Stand Up";
        }
        else
        {
            return "Sit Down";
        }
    }

    public bool IsActive()
    {
        return this.active;
    }
}
