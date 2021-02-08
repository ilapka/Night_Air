using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camThirdPerson;

    private bool thirdPerson = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            camThirdPerson.Priority = thirdPerson ? 0 : 15;
            thirdPerson = !thirdPerson;
        }
    }

    public void thirdPersonEnable()
    {
        camThirdPerson.Priority = 15;
    }

    public void thirdPersonDisable()
    {
        camThirdPerson.Priority = 0;
    }
}
