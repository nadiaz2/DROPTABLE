using System;
using System.Collections;
using System.Collections.Generic;
using SocketIOClient;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JacobDialogue : MonoBehaviour
{
    public DialogueTrigger trigger;
    private bool talkedAlready = false;

    public GameObject JacobText;
    public bool interactable;
    public chair chair;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

    }

    /*
    // Update is called once per frame
    void Update()
    {

        // Triggering Dialogue with Jacob
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit, 1000f))
            {
                if (Hit.collider.gameObject.tag == "Jacob" && !talkedAlready)
                {
                    Debug.Log("Here");
                    trigger.TriggerDialogue();
                    GameManager.state = GameState.TalkingToJacob;
                    talkedAlready = true;
                }
            }
        }
    }
    */

    void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("SeatCamera") || other.CompareTag("MainCamera")) && !talkedAlready)
        {
            JacobText.SetActive(true);
            interactable = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SeatCamera") || other.CompareTag("MainCamera"))
        {
            JacobText.SetActive(false);
            interactable = false;
        }
    }

    void Update()
    {
        if (sceneName == "Classroom") {
            if (!chair.sitting)
            {
                JacobText.SetActive(false);
            }
        }
        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !talkedAlready)
            {
                JacobText.SetActive(false);
                interactable = false;
                trigger.TriggerDialogue();
                GameManager.state = GameState.TalkingToJacob;
                talkedAlready = true;
            }
        }
    }

}
