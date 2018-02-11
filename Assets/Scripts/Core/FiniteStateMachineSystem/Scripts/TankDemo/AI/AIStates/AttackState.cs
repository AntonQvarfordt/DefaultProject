using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// When AI find the player,and the player in attack range of ai
/// </summary>
public class AttackState : AIFSMState
{
    private NavMeshAgent m_NavMeshAgent;

    private Animator m_Animator;

    private GameObject m_AttackTarget;

    //the speed we turn to the attack target
    private float m_TurrentRotateSpeed = 10.0f;

    //the frequence we attack the target
    private float m_AttackInterval = 2.0f;

    private float m_AttackIntervalTimer;

    public AttackState(string fsmStateName, AIController aiController)
        : base(fsmStateName, aiController)
    {
        this.m_AIController = aiController;
    }

    public override void OnEnter()
    {
        m_NavMeshAgent = this.m_AIController.NavMeshAgent;

        m_AttackTarget = this.m_AIController.target;

        m_Animator = this.m_AIController.Animator;
    }

    public override void OnUpdate()
    {
        //AI originalRotation
        Quaternion originalRotation = this.m_AIController.transform.rotation;

        //AI targetRotation
        Quaternion targetRotation = Quaternion.LookRotation(m_AttackTarget.transform.position - this.m_AIController.transform.position);

        //turn to the attack target
        this.m_AIController.transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, Time.deltaTime * m_TurrentRotateSpeed);

        RaycastHit hitInfo;

        //detect if the gun of AI has aimed the attack target
        bool ifHitSomething = Physics.SphereCast(this.m_AIController.transform.position,m_NavMeshAgent.radius,
            this.m_AIController.transform.forward,out hitInfo,this.m_AIController.sightRange);

        //if AI has aimed the attack target  
        if (ifHitSomething == true && hitInfo.transform.CompareTag("PlayerTank"))
        {
            if (Time.time > m_AttackIntervalTimer)
            {
                m_AttackIntervalTimer = Time.time + m_AttackInterval;

                //play the openfire animation
                m_Animator.SetTrigger("openfire");

                //if attack target has the life system,reduce it life value
                LifeSystem lifeSystem = hitInfo.transform.GetComponent<LifeSystem>();

                if (lifeSystem != null)
                {
                    lifeSystem.ChangeCurrentLifeValue(-10.0f);
                }

            }
            
        }
    }

    public override void OnExit()
    {
        m_NavMeshAgent = null;

        m_AttackTarget = null;

        m_Animator = null;
    }

}
