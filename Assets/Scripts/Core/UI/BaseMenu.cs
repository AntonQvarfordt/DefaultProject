using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMenu : FSMEntry
{
    public GameObject Root;
    public BaseMenu(string fsmStateName, GameObject root) : base(fsmStateName)
    {
        Root = root;
    }
}
