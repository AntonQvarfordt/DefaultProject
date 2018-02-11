using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShowName : MonoBehaviour
{
    public Transform followTarget;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        transform.position = new Vector3(followTarget.position.x, transform.position.y, followTarget.position.z);
    }
}
