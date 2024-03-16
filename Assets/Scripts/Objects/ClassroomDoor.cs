using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassroomDoor : MonoBehaviour, Interactable
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
        this.active = (ClassroomManager.state == ClassroomState.ClassOver) || (ClassroomManager.state == ClassroomState.FinishedTalkMorgan) || (ClassroomManager.state == ClassroomState.Day2ClassOver);
    }

    public void Interact()
    {
        switch (ClassroomManager.state)
        {
            case ClassroomState.ClassOver:
                GameManager.state = GameState.OnWayHomeStart;
                SceneManager.LoadScene("OnWayHome");
                break;

            case ClassroomState.FinishedTalkMorgan:
                GameManager.state = GameState.ReturningHomeAfterHeadphones;
                SceneManager.LoadScene("OnWayHome");
                break;

            case ClassroomState.Day2ClassOver:
                GameManager.state = GameState.Day2HeadBackHome;
                SceneManager.LoadScene("OnWayHome");
                break;
        }
    }


    public string GetPrompt()
    {
        return "Leave Classroom";
    }

    public bool IsActive()
    {
        return this.active;
    }
}
