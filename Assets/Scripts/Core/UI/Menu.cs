using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Menu : FSMState
{
    public GameObject Root;
    public Menu(string fsmStateName, GameObject root) : base(fsmStateName)
    {
        Root = root;
    }

}
