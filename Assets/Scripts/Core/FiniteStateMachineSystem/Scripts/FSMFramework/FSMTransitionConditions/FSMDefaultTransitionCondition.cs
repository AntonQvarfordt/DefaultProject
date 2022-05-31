﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the default condition,the CheckCondition function always return the true value
/// </summary>
public class FSMDefaultTransitionCondition : IFSMTransitionCondition
{
    public bool CheckCondition()
    {
        return true;
    }
}
