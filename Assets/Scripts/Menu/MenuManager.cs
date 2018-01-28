using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();        
    }

    public void ConfirmMenuClick ()
    {
        _canvasGroup.blocksRaycasts = false;
    }
    public void MenuTransitioned()
    {
        _canvasGroup.blocksRaycasts = true;
    }

}
