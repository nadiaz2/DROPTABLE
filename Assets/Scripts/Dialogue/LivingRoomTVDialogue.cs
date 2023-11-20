using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomTVDialogue : MonoBehaviour
{

    public DialogueTrigger trigger;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit, 100f))
            {
                if (Hit.collider.gameObject.tag == "TV")
                {
                    trigger.TriggerDialogue();
                }
            }
        }
    }

}


    