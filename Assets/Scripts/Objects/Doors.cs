using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{

    public GameObject enterText;
    public bool interactable;

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
        }
    }

}
