using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OptionsMenuState : Menu
{
    public OptionsMenuState(string fsmStateName, GameObject root) : base(fsmStateName, root)
    {
    }

    public override bool OnCheckTransition()
    {
        return base.OnCheckTransition();
    }

    public override void OnEnter()
    {
        Root.SetActive(true);
        Root.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
    }

    public override void OnExit()
    {
        Root.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() => Root.SetActive(false));
        
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
