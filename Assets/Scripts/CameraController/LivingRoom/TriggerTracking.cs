using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTracking : MonoBehaviour
{
    public TriggerTracking otherTrigger;
    public Camera mainCamera;
    public Transform oppositeCameraPosition;
    private bool playerIn = false;


    private void OnTriggerEnter(Collider other)
    {  
        playerIn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerIn = false;
        if (otherTrigger.playerIn)
        {
            mainCamera.transform.SetPositionAndRotation(oppositeCameraPosition.position, oppositeCameraPosition.rotation);
        }
    }

}
