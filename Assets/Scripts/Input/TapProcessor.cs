using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TapProcessor : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent[] ExecuteOnTap;

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (UnityEvent uEvent in ExecuteOnTap)
        {
            uEvent.Invoke();
        }
    }
}
