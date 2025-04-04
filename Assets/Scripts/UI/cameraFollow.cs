using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    void Start()
    {
        vcam.Follow = GameObject.FindWithTag("Player").transform;
    }

}
