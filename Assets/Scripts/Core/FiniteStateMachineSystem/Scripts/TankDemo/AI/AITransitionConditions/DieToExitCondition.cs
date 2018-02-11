using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the condition that the ai switch the die state in fsm to the exit state
/// </summary>
public class DieToExitCondition : AIFSMTransitionCondition
{
    public DieToExitCondition(AIController aiController) : base(aiController) { }

    public override bool CheckCondition()
    {
        bool allDone = true;

        //check if all the explosion effect play finished,if finished,return true.
        //otherwise,ai tank stay in this die state
        for (int i = 0; i < this.m_AIController.ParticleSystemArray.Length;i++ )
        {
            ParticleSystem particleSystem = this.m_AIController.ParticleSystemArray[i];

            if (particleSystem.isPlaying == true)
            {
                allDone = false;

                break;
            }
        }

        return allDone;
    }
}
