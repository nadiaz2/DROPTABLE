using System;
using System.Collections;
using System.Collections.Generic;
using SocketIOClient;
using UnityEngine;

public class JacobDialogue : MonoBehaviour
{

    public DialogueTrigger trigger;

    private Vector3 startPosition;
    public Vector3 finalPosition;
    private float lerpPercent;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        lerpPercent = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (PhoneFoundDialogue.photoFound)
        {
            this.transform.position = Vector3.Lerp(startPosition, finalPosition, lerpPercent);
            lerpPercent = Math.Max(lerpPercent - moveSpeed, 0.0f);

        } 
        else if (GameManager.state == GameState.FinishedTalking)
        {
            this.transform.position = Vector3.Lerp(startPosition, finalPosition, lerpPercent);
            lerpPercent = Math.Min(lerpPercent + moveSpeed, 1.0f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit, 100f))
            {
                if (Hit.collider.gameObject.tag == "Jacob")
                {
                    trigger.TriggerDialogue();
                    GameManager.state = GameState.TalkingToJacob;
                }
            }
        }
    }

}
