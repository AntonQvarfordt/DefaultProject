using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the condition that the ai switch the patrol state to the chase state
/// </summary>
public class PatrolToChaseCondition : AIFSMTransitionCondition
{
    public PatrolToChaseCondition(AIController aiController) : base(aiController) { }

    public override bool CheckCondition()
    {
        GameObject[] tankArray = m_AIController.PlayerTanksInScene;

        bool returnValue = false;

        //check all player tank in scene
        for (int i = 0; i < tankArray.Length; i++)
        {
            GameObject playerTank = tankArray[i];
            
            //if current player tank in the sight view of ai,then ai select this player tank as chase target,and return true
            //otherwise,ai tank stay in this patrol state
            if (Vector3.Distance(this.m_AIController.transform.position, playerTank.transform.position) <= this.m_AIController.sightRange
                && Vector3.Distance(this.m_AIController.transform.position, playerTank.transform.position) > this.m_AIController.attackRange
                && Vector3.Angle((playerTank.transform.position - this.m_AIController.transform.position), this.m_AIController.transform.forward) <= this.m_AIController.fieldOfView/2.0f)
            {

                this.m_AIController.target = playerTank;

                returnValue = true;

                break;
            }
        }

        return returnValue;
    }
}
