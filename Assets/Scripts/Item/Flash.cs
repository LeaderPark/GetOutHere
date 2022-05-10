using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : OVRGrabbable
{
    [SerializeField]
    private Light _light;

    protected override void Start()
    {
        _light.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isGrabbed)
        {
            On();
        }
        else
        {
            Off();
        }
    }

    void On()
    {
        _light.gameObject.SetActive(true);
    }

    void Off()
    {
        _light.gameObject.SetActive(false);

    }
}