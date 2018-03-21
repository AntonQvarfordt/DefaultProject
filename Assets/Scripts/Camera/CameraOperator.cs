using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOperator : MonoBehaviour {

    public CinemachineBrain CinemaBrain;

    public CinemachineVirtualCamera MainCamera;
    public CinemachineVirtualCamera FollowCamera;

    public Transform FollowTarget;

    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            SetFollowCameraTarget(FollowTarget, true);
        }
        if (Input.GetKeyDown("p"))
        {
            EnableMainCamera();
        }
    }

    public void SetFollowCameraTarget (Transform target, bool cutToFollowCamera = false)
    {
        FollowCamera.Follow = target;
        FollowCamera.LookAt = target;

        if (cutToFollowCamera)
        {
            EnableFollowCamera();
        }
    }

    public void EnableFollowCamera ()
    {
        FollowCamera.gameObject.SetActive(true);
        MainCamera.gameObject.SetActive(false);
    }

    public void EnableMainCamera ()
    {
        MainCamera.gameObject.SetActive(true);
        FollowCamera.gameObject.SetActive(false);
    }

}
