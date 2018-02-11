using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// If the AI see the player,it will chase player
/// </summary>
public class ChaseState : AIFSMState
{
    private NavMeshAgent m_NavMeshAgent;

    private GameObject m_ChaseTarget;

    public ChaseState(string fsmStateName, AIController aiController)
        : base(fsmStateName, aiController)
    {
        this.m_AIController = aiController;
    }

    public override void OnEnter()
    {
        m_NavMeshAgent = this.m_AIController.NavMeshAgent;

        m_ChaseTarget = this.m_AIController.target;

        //the current position of the target that the ai see
        Vector3 targetPos = m_ChaseTarget.transform.position;

        if (m_NavMeshAgent.enabled == false)
        {
            m_NavMeshAgent.enabled = true;
        }

        //notify the ai to chase the target
        m_NavMeshAgent.SetDestination(targetPos);
    }

    public override void OnUpdate()
    {
        //the current position of the target that the ai see
        Vector3 targetPos = m_ChaseTarget.transform.position;

        //notify the ai to chase the target
        m_NavMeshAgent.SetDestination(targetPos);
    }

    public override void OnExit()
    {
        if (m_NavMeshAgent.enabled == true)
        {
            m_NavMeshAgent.enabled = false;
        }

        m_NavMeshAgent = null;

        m_ChaseTarget = null;
    }
}
