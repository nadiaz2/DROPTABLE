using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera2D : MonoBehaviour
{
    public Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
        if (!DialogueManager.dialogueOngoing && !PhoneClicked.onPhone)
        {
            transform.position.Set(cameraPosition.position.x, cameraPosition.position.y, cameraPosition.position.z);
        }
    }
}
