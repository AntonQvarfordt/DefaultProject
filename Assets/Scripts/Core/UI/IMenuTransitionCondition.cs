using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMenuTransitionCondition : IFSMTransitionCondition
{
    Object passedObject;

    public IMenuTransitionCondition(Object pObj)
    {
        passedObject = pObj;
        Debug.Log(passedObject);
    }

    //default return false,means don't switch state
    public virtual bool CheckCondition()
    {
        Debug.Log("CheckCond");
        return false;
    }


}
