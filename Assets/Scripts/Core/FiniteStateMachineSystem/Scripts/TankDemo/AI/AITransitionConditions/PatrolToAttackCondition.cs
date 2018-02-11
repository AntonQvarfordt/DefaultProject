using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the condition that the ai switch the patrol state to the attack state
/// </summary>
public class PatrolToAttackCondition : AIFSMTransitionCondition
{
    public PatrolToAttackCondition(AIController aiController) : base(aiController) { }

    //check all player tank in scene
    public override bool CheckCondition()
    {
        GameObject[] tankArray = m_AIController.PlayerTanksInScene;

        bool returnValue = false;

        for (int i = 0; i < tankArray.Length; i++)
        {
            GameObject playerTank = tankArray[i];

            //if current player tank in the sight view of ai and in its attack range,then ai select this player tank as the attack target,and return true
            //otherwise,ai tank stay in this patrol state
            if (Vector3.Distance(m_AIController.transform.position, playerTank.transform.position) <= m_AIController.attackRange
                && Vector3.Angle((playerTank.transform.position - m_AIController.transform.position), m_AIController.transform.forward) <= m_AIController.fieldOfView / 2.0f)
            {
                this.m_AIController.target = playerTank;

                returnValue = true;

                break;
            }

        }

        return returnValue;


    }

}
