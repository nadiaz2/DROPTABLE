using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class PhoneClicked : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 finalPosition;
    private Quaternion finalRotation;
    private float lerpPercent;
    private bool phoneMoving;

    private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;
        lerpPercent = 0.0f;
        phoneMoving = false;
        

        camera = GameObject.Find("PlayerCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(phoneMoving) {
            this.transform.position = Vector3.Lerp(startPosition, finalPosition, lerpPercent);
            this.transform.rotation = Quaternion.Slerp(startRotation, finalRotation, lerpPercent);
            lerpPercent = Math.Min(lerpPercent + 0.005f, 1.0f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit, 1000f))
            {
                if (Hit.collider.gameObject.tag == "Phone")
                {
                    phoneMoving = true;
                    finalPosition = camera.transform.position + camera.transform.forward * 10f;

                    Vector3 rot = camera.transform.rotation.eulerAngles;
                    finalRotation = camera.transform.rotation * Quaternion.AngleAxis(180, Vector3.up);

                    DisplayQRCode();
                }
            }
        }
    }

    private void DisplayQRCode()
    {
        Debug.Log("Hi");
    }
}
