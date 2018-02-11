using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// this the life system,record the life value of the unit in scene
/// </summary>
public class LifeSystem : MonoBehaviour
{
    public float initLifeValue = 100.0f;

    public float CurrentLifeValue{set;get;}

    /// <summary>
    /// to change the life value
    /// </summary>
    /// <param name="deltaValue"></param>
    public void ChangeCurrentLifeValue(float deltaValue)
    {
        CurrentLifeValue = CurrentLifeValue + deltaValue;

        if (CurrentLifeValue <= 0.0f)
        {
            CurrentLifeValue = 0.0f;
        }
    }

    void Awake()
    {
        CurrentLifeValue = initLifeValue;
    }
}
