using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomNPC : MonoBehaviour
{
    public SplineMovement movement;
    public ClassroomState movementStart;

    private bool moved;

    [Header("Animator")]
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        moved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if((ClassroomManager.state != movementStart) || moved)
        {
            if(movement.moving)
            {
                animator.SetBool("IsWalking", true);
            }
            return;
        }

        movement.StartMovement();
        moved = true;

        /*
        if (ClassroomManager.state == ClassroomState.ClassOver && !studentsMoved)
        {
            Move();
            Debug.Log("Moved");
        }
        if (ClassroomManager.state == ClassroomState.PickedUpHeadphones && !studentsMoved)
        {
            MorganMove();
        }
        */
    }



    void MorganMove()
    {
        //agent.SetDestination(morganWaypoint.position);
    }


}
