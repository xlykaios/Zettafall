using UnityEngine;
using Cinemachine;
using Cinemachine.Utility;
using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class CameraTriggerBox : MonoBehaviour
{
    public GameObject vcam;

    void OnTriggerEnter(Collider other)
    {
        switchToThisVcam();
    }


    void OnTriggerExit(Collider other)
    {
        switchVcamOff();
    }


    public void switchToThisVcam()
    {

        vcam.gameObject.SetActive(true);
    }

    public void switchVcamOff()
    {

        vcam.gameObject.SetActive(false);
    }


}
