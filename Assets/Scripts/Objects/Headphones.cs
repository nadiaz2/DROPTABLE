using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Headphones : MonoBehaviour
{

    public GameObject pickupText;
    public bool interactable;

    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("MainCamera"))
        {
            pickupText.SetActive(true);
            interactable = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            pickupText.SetActive(false);
            interactable = false;
        }
    }

    private void Update()
    {
        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.state = GameState.PickedupHeadphones;
                gameObject.SetActive(false);
                pickupText.SetActive(false);
            }
        }
    }
}
