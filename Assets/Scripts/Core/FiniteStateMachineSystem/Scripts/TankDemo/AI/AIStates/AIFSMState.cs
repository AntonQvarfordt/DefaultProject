using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base state of AI tank
/// </summary>
public class AIFSMState : FSMState
{
    protected AIController m_AIController;

    public AIFSMState(string fsmStateName, AIController aiController)
        : base(fsmStateName)
    {
        this.m_AIController = aiController;
    }
}
