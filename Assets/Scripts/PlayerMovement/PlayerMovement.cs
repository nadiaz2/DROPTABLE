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
    private Rigidbody _rb;
    public Rigidbody rb
    {
        get
        {
            return this._rb;
        }
    }

    // If the player is currently frozen due to movement being restricted (sitting, on phone, etc.)
    // Other scripts will generally set this value
    public bool immobile { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _rb.drag = groundDrag;

        forward = Vector3.ProjectOnPlane(movementReference.forward, Vector3.up).normalized;
        right = Vector3.ProjectOnPlane(movementReference.right, Vector3.up).normalized;

        immobile = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (immobile)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        SpeedControl();
        MyInput();
    }

    private void FixedUpdate()
    {
        if (immobile)
        {
            _rb.velocity = Vector3.zero;
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
        Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }

    private void MovePlayer()
    {
        // calcaulate movement direction
        moveDirection = (forward * verticalInput) + (right * horizontalInput);
        _rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        Vector3 facing = _rb.velocity.normalized;
        facing.y = 0;
        if (facing.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(facing);
        }
    }
}
