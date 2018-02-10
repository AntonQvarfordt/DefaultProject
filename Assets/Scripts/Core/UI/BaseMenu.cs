using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMenu : Menu
{
    public Menu ActiveMenu;

    public Dictionary<string, Button> IndependentButtons = new Dictionary<string, Button>();
}
