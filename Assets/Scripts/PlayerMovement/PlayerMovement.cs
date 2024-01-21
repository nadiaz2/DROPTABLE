using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Transform movementReference;
    public float moveSpeed;
    public float groundDrag;

    // Movement Axis
    private Vector3 forward;
    private Vector3 right;

    // Input values
    float horizontalInput;
    float verticalInput;

    // Current Movement Values
    private Vector3 moveDirection;
    private Rigidbody rb;

    // If the player is currently frozen due to movement being restricted (sitting, on phone, etc.)
    // Other scripts will generally set this value
    public bool immobile { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = groundDrag;

        forward = Vector3.ProjectOnPlane(movementReference.forward, Vector3.up).normalized;
        right = Vector3.ProjectOnPlane(movementReference.right, Vector3.up).normalized;

        immobile = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (immobile)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        SpeedControl();
        MyInput();
    }

    private void FixedUpdate()
    {
        if (immobile)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void MovePlayer()
    {
        // calcaulate movement direction
        moveDirection = (forward * verticalInput) + (right * horizontalInput);
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        
        Vector3 facing = rb.velocity.normalized;
        facing.y = 0;
        if(facing.magnitude > 0) {
            transform.rotation = Quaternion.LookRotation(facing);
        }
    }
}



