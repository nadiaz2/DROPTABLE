using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorganCloseUp : MonoBehaviour
{

    public GameObject playerCamera, morgan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Morgan" && GameManager.state == GameState.PickedupHeadphones)
        {
            Invoke("returnToPlayerCamera", 5);
            morgan.SetActive(true);
            playerCamera.SetActive(false);
            GameManager.state = GameState.MorganCloseUp;
        }
    }

    void returnToPlayerCamera()
    {
        playerCamera.SetActive(true); ;
        morgan.SetActive(false); ;
    }


}
