using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : StateMachineBehaviour {

    private CanvasGroup _canvasGroup;


    public void ConfirmMenuClick ()
    {
        _canvasGroup.blocksRaycasts = false;
    }
    public void MenuTransitioned()
    {
        _canvasGroup.blocksRaycasts = true;
    }

}
