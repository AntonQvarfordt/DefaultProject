using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This state is the first state when the finite state machine system working and you can not change it
/// </summary>
public class FSMEntry : FSMState
{
    public FSMEntry(string fsmStateName) : base(fsmStateName) { }

}
