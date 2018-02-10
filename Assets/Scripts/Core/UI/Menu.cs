using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Menu : MonoBehaviour {

    public abstract void Open();

    public abstract void TransitionIn(Action onComplete);

    public abstract void TransitionOut(Action onComplete);
}
