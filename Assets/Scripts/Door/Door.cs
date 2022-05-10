using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorGrabber;

    public bool isOpen = true;

    private void Awake()
    {
        if (!isOpen)
        {
            doorGrabber.SetActive(false);
        }
    }


    private void Update()
    {
        if (isOpen && doorGrabber.activeSelf == false)
        {
            doorGrabber.SetActive(true);
        }
    }
}
