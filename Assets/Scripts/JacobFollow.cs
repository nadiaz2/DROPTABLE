using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JacobFollow : MonoBehaviour
{
    [Header("Objects")]
    public Transform player;
    private NavMeshAgent agent;

    [Header("Animator")]
    public Animator animator;

    [Header("NavMeshController")]
    public int waitFrames;
    private int stillFrames = 0;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);


        if(agent.velocity.magnitude < 5f)
        {
            stillFrames = Math.Min(stillFrames+1, waitFrames);
        }
        else
        {
            stillFrames = 0;
        }


        if (stillFrames >= waitFrames)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsStanding", true);
        }
        else
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsStanding", false);
        }
    }
}
