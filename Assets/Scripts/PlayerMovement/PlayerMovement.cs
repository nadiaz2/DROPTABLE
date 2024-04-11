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

    [Header("Animator")]
    public Animator animator;

    public bool seated = false;

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

    private Transform teleportLocation = null;
    public void TeleportPlayer(Transform loc)
    {
        teleportLocation = loc;
    }


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _rb.drag = groundDrag;

        immobile = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(teleportLocation != null)
        {
            transform.SetPositionAndRotation(teleportLocation.position, teleportLocation.rotation);
            teleportLocation = null;
        }

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
            if (animator != null && seated)
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsStanding", false);
                animator.SetBool("IsSitting", true);
            }

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
        Vector3 forward = Vector3.ProjectOnPlane(movementReference.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(movementReference.right, Vector3.up).normalized;

        // calcaulate movement direction
        moveDirection = (forward * verticalInput) + (right * horizontalInput);
        _rb.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);

        Vector3 facing = _rb.velocity.normalized;
        facing.y = 0;
        if (facing != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(facing);
        }

        if (animator != null)
        {
            if (_rb.velocity.magnitude > 0.0f)
            {
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsStanding", false);
                animator.SetBool("IsSitting", false);
                seated = false;
            }
            else
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsStanding", true);
                animator.SetBool("IsSitting", false);
                seated = false;
            }

        }
    }
}
