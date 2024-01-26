using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ClassroomNPCNavigation : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform[] waypoints;
    private int waypointIndex;
    public Transform morganWaypoint;

    private bool studentsMoved = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        waypointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.ClassOver && !studentsMoved)
        {
            Move();
            Debug.Log("Moved");
        }
        if (GameManager.state == GameState.PickedupHeadphones && !studentsMoved)
        {
            MorganMove();
        }
    }

    void Move()
    {
        if (waypointIndex >= waypoints.Length)
        {
            studentsMoved = true;
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

    void MorganMove()
    {
        agent.SetDestination(morganWaypoint.position);
    }


}
