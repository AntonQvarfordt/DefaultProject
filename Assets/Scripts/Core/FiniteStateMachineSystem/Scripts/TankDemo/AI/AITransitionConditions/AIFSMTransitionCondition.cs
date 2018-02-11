using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the base class of all the other ai tank condition
/// </summary>
public class AIFSMTransitionCondition : IFSMTransitionCondition
{
    protected AIController m_AIController;

    public AIFSMTransitionCondition(AIController aiController)
    {
        this.m_AIController = aiController;
    }

    //default return false,means don't switch state
    public virtual bool CheckCondition()
    {
        return false;
    }
}
