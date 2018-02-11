using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the condition that the ai switch the chase state to the attack state
/// </summary>
public class ChaseToAttackCondition : AIFSMTransitionCondition
{
    public ChaseToAttackCondition(AIController aiController) : base(aiController) { }

    public override bool CheckCondition()
    {
        //if the chase target access the attack range of ai tank,we return true to switch to the attack state
        //otherwise,ai tank stay in this chase state
        if (Vector3.Distance(m_AIController.transform.position, m_AIController.target.transform.position) <= m_AIController.attackRange
                 && Vector3.Angle((m_AIController.target.transform.position - m_AIController.transform.position), m_AIController.transform.forward) <= m_AIController.fieldOfView / 2.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
           
    }
}
