using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    public DialogueTrigger trigger;

    // Update is called once per frame
    void Update()
    {
        if (!trigger.dialogueStart)
        {
            transform.position = cameraPosition.position;
        }
    }
}
