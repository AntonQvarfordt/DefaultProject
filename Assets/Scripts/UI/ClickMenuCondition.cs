using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickMenuCondition : IMenuTransitionCondition
{
    bool isClicked;
    Button passedButton;
    public ClickMenuCondition(Object passedObject) : base(passedObject) {
        passedButton = (Button)passedObject;
        passedButton.onClick.AddListener(OnClick);
    }

    public override bool CheckCondition()
    {
        if (isClicked)
        {
            isClicked = false;
            return true;
        }

        return false;
    }

    private void OnClick ()
    {
        Debug.Log("OnClick");
        isClicked = true;
    }
}
