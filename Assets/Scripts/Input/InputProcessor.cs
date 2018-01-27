using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PhysicsRaycaster))]
public class InputProcessor : MonoBehaviour {

    //private PhysicsRaycaster _raycaster;
    //private Physics2DRaycaster _raycaster2D;
    //private StandaloneInputModule _inputModule;

    //public List<string> PhysicsRaycastList = new List<string>();
    //public List<string> Physics2DRaycastList = new List<string>();

    public List<GameObject> SelectionHistory;
    public GameObject Selected;
    public GameObject LastSelected;


    private void Awake()
    {
        //_raycaster = GetComponent<PhysicsRaycaster>();
        //_raycaster2D = GetComponent<Physics2DRaycaster>();
        //_inputModule = FindObjectOfType<StandaloneInputModule>();
    }

    public void TapDebug (GameObject go)
    {
        Debug.Log("Tap " + go);
    }

    public void SetSelectedObject (GameObject go)
    {
        SelectionHistory.Add(go);
        RefreshSelections();

    }

    public void Deselect ()
    {
        Selected = null;
    }

    private void RefreshSelections ()
    {
        if (SelectionHistory.Count > 10)
        {
            SelectionHistory.RemoveAt(0);
        }

        if (SelectionHistory.Count > 0)
        Selected = SelectionHistory[SelectionHistory.Count - 1];
        if (SelectionHistory.Count > 1)
            LastSelected = SelectionHistory[SelectionHistory.Count - 2];
    }
}
