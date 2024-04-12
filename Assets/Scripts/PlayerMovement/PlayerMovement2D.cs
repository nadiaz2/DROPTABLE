using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerMovement2D : MonoBehaviour
{
 [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public Transform orientation;


    private float horizontalInput;

    private Vector3 moveDirection;

    [Header("Animator")]
    public Animator animator;

    private Rigidbody _rb;
    public Rigidbody rb
    {
        get
        {
            return this._rb;
        }
    }


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
    }

    // Update is called once per frame
    void Update()
    {
        if (teleportLocation != null)
        {
            transform.SetPositionAndRotation(teleportLocation.position, teleportLocation.rotation);
            teleportLocation = null;
        }

        if (DialogueManager.dialogueOngoing)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        SpeedControl();
        MyInput();
    }

    private void FixedUpdate()
    {
        if (DialogueManager.dialogueOngoing)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void MovePlayer()
    {

        Vector3 right = Vector3.ProjectOnPlane(orientation.right, Vector3.up).normalized;

        // calcaulate movement direction
        moveDirection = right * horizontalInput;
        _rb.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);


        Vector3 facing = _rb.velocity;
        facing.x = 0;
        facing.y = 0;
        if (facing.magnitude >= 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(facing.normalized);
        }

        if (animator != null)
        {
            if (_rb.velocity.magnitude > 0.0f)
            {
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsStanding", false);
                animator.SetBool("IsSitting", false);
            }
            else
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsStanding", true);
                animator.SetBool("IsSitting", false);
            }

        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(0f, 0f, _rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }
}
