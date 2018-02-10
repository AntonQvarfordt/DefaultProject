using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ITransition {

    void TransitionIn(Action onComplete);
    void TransitionOut(Action onComplete);
}
