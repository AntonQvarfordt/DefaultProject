using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the condition that the ai switch the attack state to the patrol state
/// </summary>
public class AttackToPatrolCondition : AIFSMTransitionCondition
{
    public AttackToPatrolCondition(AIController aiController) : base(aiController) { }

    public override bool CheckCondition()
    {
        //if the attack target died or out of ai sight,we return true to switch to the patrol state
        // otherwise,ai tank stay in this attack state
        if (m_AIController.target == null)
        {
            return true;
        }
        else if (Vector3.Distance(m_AIController.transform.position, m_AIController.target.transform.position) > m_AIController.sightRange
                 || Vector3.Angle((m_AIController.target.transform.position - m_AIController.transform.position), m_AIController.transform.forward) > m_AIController.fieldOfView / 2.0f)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
