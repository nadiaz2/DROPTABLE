using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JacobDialogue : MonoBehaviour
{

    public DialogueTrigger trigger;

    public static bool JacobTalked = false;

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
                if (Hit.collider.gameObject.tag == "Jacob")
                {
                    trigger.TriggerDialogue();
                    JacobTalked = true;
                }
            }
        }
    }

}
