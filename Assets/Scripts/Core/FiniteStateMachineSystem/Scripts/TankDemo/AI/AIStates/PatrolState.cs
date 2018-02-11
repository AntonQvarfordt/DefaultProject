using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// The patrol state class,make the ai tank follow a few points to move 
/// </summary>
public class PatrolState : AIFSMState
{
    //patrol waypoints
    private Transform[] m_PatrolWayPointArray;

    private NavMeshAgent m_NavMeshAgent;
    
    //the current index of patrol points
    private int m_WayPointCurrentIndex;


    public PatrolState(string fsmStateName, AIController aiController)
        : base(fsmStateName, aiController)
    {
        this.m_AIController = aiController;
    }

    public override void OnEnter()
    {
        m_PatrolWayPointArray = this.m_AIController.patrolWayPointArray;

        m_NavMeshAgent = this.m_AIController.NavMeshAgent;

        //the position of the way point we get from the waypoint array
        Vector3 destinationPos = m_PatrolWayPointArray[m_WayPointCurrentIndex].position;

        if(m_NavMeshAgent.enabled == false)
        {
            m_NavMeshAgent.enabled = true;
        }

        //notify the ai to move to the position of waypoint
        m_NavMeshAgent.SetDestination(destinationPos);
        
    }

    public override void OnUpdate()
    {
        // Check if we've reached the destination
        if (m_NavMeshAgent.pathPending == false && m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
        {
            if (m_NavMeshAgent.hasPath == false || m_NavMeshAgent.velocity.sqrMagnitude == 0.0f)
            {
                // if ai reached the waypoint,then move to the next waypoint
                m_WayPointCurrentIndex++;

                m_WayPointCurrentIndex = m_WayPointCurrentIndex % m_PatrolWayPointArray.Length;

                //get the next waypoint
                Vector3 destinationPos = m_PatrolWayPointArray[m_WayPointCurrentIndex].position;

                //notify the ai to move to the position of next waypoint
                m_NavMeshAgent.SetDestination(destinationPos);
            }
        }
    }

    public override void OnExit()
    {
        if (m_NavMeshAgent.enabled == true)
        {
            m_NavMeshAgent.enabled = false;
        }

        m_PatrolWayPointArray = null;

        m_NavMeshAgent = null;
    }
}
