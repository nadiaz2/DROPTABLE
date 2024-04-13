using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class SplineMovement : MonoBehaviour
{
    public SplineContainer splineContainer;
    public int splineIndex;
    public float speed;
    public bool reverseMovement;
    public LayerMask groundLayers;

    private float splineLength;
    private bool animate;
    private float distAmount;

    private bool _moving;
    public bool moving
    {
        get
        {
            return _moving;
        } 
    }


    // Start is called before the first frame update
    void Start()
    {
        splineLength = splineContainer.CalculateLength(splineIndex);
        _moving = false;
        animate = false;
        distAmount = 0.0f;
        //Debug.Log($"{ splineContainer.EvaluatePosition(splineIndex, 0.01f)}");
    }

    // Update is called once per frame
    void Update()
    {
        if(!animate || (distAmount >= splineLength) || (distAmount <= 0.0f))
        {
            _moving = false;
            return;
        }
        _moving = true;

        float mult = reverseMovement ? -1.0f : 1.0f;
        distAmount += mult * Time.deltaTime * speed;
        float percentPos = Mathf.Clamp(distAmount / splineLength, 0.0f, 1.0f);

        //float usePercent = reverseMovement ? (1.0f-percentPos) : percentPos;

        float3 splinePosition = splineContainer.EvaluatePosition(splineIndex, percentPos);
        float3 splineTangent = splineContainer.EvaluateTangent(splineIndex, percentPos);

        RaycastHit Hit;
        Physics.Raycast(splinePosition, -Vector3.up, out Hit, 500.0f, groundLayers);

        Vector3.ProjectOnPlane(new Vector3(), Vector3.up);

        transform.position = new Vector3(splinePosition.x, Hit.point.y, splinePosition.z);
        transform.LookAt(transform.position + new Vector3(mult * splineTangent.x, 0.0f, mult * splineTangent.z));
    }

    public void StartMovement() {
        this.animate = true;
    }

    public void StopMovement()
    {
        this.animate = false;
    }
}
