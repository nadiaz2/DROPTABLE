using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chair : MonoBehaviour
{
    public GameObject playerStanding, playerSitting, intText, standText;
    public bool interactable, sitting;
    public Camera standingCamera, sittingCamera;

    void OnTriggerStay(Collider other)
    {
        Debug.Log($"OnTriggerStay: {GameManager.state}");

        if (other.CompareTag("MainCamera") && GameManager.state != GameState.BackToClassroom)
        {
            intText.SetActive(true);
            interactable = true;
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false);
            interactable = false;
        }
    }
    void Update()
    {
        if (sitting)
        {
            intText.SetActive(false);
        }

        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.E) && GameManager.state != GameState.BackToClassroom)
            {
                intText.SetActive(false);
                standText.SetActive(true);
                playerSitting.SetActive(true);
                sitting = true;
                playerStanding.SetActive(false);
                interactable = false;
                standingCamera.GetComponent<AudioListener>().enabled = false;
                sittingCamera.GetComponent<AudioListener>().enabled = true;
                GameManager.state = GameState.ClassRoomSubtitleEnd;

            }
        }
        if (sitting)
        {
            if (GameManager.state != GameState.FinishedTalking)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    playerSitting.SetActive(false);
                    standText.SetActive(false);
                    playerStanding.SetActive(true);
                    sitting = false;
                    standingCamera.GetComponent<AudioListener>().enabled = true;
                    sittingCamera.GetComponent<AudioListener>().enabled = false;
                }
            }
        }
    }
}

