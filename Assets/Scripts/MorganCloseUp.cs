using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorganCloseUp : MonoBehaviour
{

    public GameObject playerCamera, morganCamera;

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
        if (other.gameObject.tag == "Morgan" && ClassroomManager.state == ClassroomState.PickedUpHeadphones)
        {
            Invoke("returnToPlayerCamera", 5);
            morganCamera.SetActive(true);
            playerCamera.SetActive(false);
            this.gameObject.SetActive(false);
            ClassroomManager.state = ClassroomState.MorganCloseUp;
        }

    }

    void returnToPlayerCamera()
    {
        playerCamera.SetActive(true);
        morganCamera.SetActive(false);
        this.gameObject.SetActive(true);
        ClassroomManager.state = ClassroomState.AfterHeadphones;
    }


}
