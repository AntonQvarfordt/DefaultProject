using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the condition that the ai switch the attack state to the chase state
/// </summary>
public class AttackToChaseCondition:AIFSMTransitionCondition
{

    public AttackToChaseCondition(AIController aiController) : base(aiController) { }
    
    public override bool CheckCondition()
    {
        //if the attack target don't in the attack range of the ai tank,we return true to switch to the chase state
        //otherwise,ai tank stay in this attack state
        if (Vector3.Distance(m_AIController.transform.position, m_AIController.target.transform.position) > m_AIController.attackRange
            && Vector3.Distance(m_AIController.transform.position, m_AIController.target.transform.position) <= m_AIController.sightRange
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
