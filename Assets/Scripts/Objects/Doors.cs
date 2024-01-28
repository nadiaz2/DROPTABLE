using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour, Interactable
{

    public string sceneName;
    public ClassroomState enabledStates;

    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        this.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(enabledStates.HasFlag(ClassroomManager.state))
        {
            this.active = true;
        }
        else
        {
            this.active = false;
        }
    }

    //public GameObject enterText;
    //public bool interactable;

    /*
    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("MainCamera"))
        {
            enterText.SetActive(true);
            interactable = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            enterText.SetActive(false);
            interactable = false;
        }
    }

    private void Update()
    {
        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "TomsDoor")
            {
                SceneManager.LoadScene("TomsRoom");
            }
            if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "JacobsDoor")
            {
                SceneManager.LoadScene("JacobsRoom");
            }
            if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "LivingRoomDoor")
            {
                SceneManager.LoadScene("LivingRoom");
            }
            if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "OnWayHomeDoor" && ClassroomManager.state == ClassroomState.ClassOver)
            {
                SceneManager.LoadScene("OnWayHome");
            }
        }
    }
    */

    public void Interact()
    {
        SceneManager.LoadScene(sceneName);
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
