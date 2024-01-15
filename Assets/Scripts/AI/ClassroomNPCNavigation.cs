using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ClassroomNPCNavigation : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform[] waypoints;
    private int waypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        waypointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (waypointIndex >= waypoints.Length)
        {
            return;
        }

        Vector3 usProjection = Vector3.ProjectOnPlane(transform.position, Vector3.up);
        Vector3 goalProjection = Vector3.ProjectOnPlane(waypoints[waypointIndex].position, Vector3.up);
        if ((usProjection - goalProjection).magnitude >= 0.001)
        {
            agent.SetDestination(waypoints[waypointIndex].position);
        }
        else
        {
            waypointIndex++;
        }
    }

}
