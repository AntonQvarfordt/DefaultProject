using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the condition that the ai switch current state in fsm to the die state
/// </summary>
public class AnyToDieCondition : AIFSMTransitionCondition
{
    public AnyToDieCondition(AIController aiController) : base(aiController) { }


    public override bool CheckCondition()
    {
        //if ai life value reduce to zero,ai switch to the die state,
        //otherwise stay current state in FSM
        if (m_AIController.LifeSystem.CurrentLifeValue <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
