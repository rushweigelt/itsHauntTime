﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RemoteElectronic : InteractableObject
{
    public bool isOn;

    /// <summary>
    /// Event called when unplugged
    /// </summary>
    public UnityEvent onPoweredOn;

    /// <summary>
    /// Event called when plugged in
    /// </summary>
    public UnityEvent onPoweredOff;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Add default listeners to plug events
        onPoweredOn.AddListener(() => SetActive(true));
        onPoweredOff.AddListener(() => SetActive(false));
    }

    //the most skeleton of an electronic, this simply toggles it on and off
    //More complex, specific code will be used in a class-down. See Fan for an example
    public virtual void SetActive(bool active)
    {
        // Debug.Log("Electronic.SetActive(" + active + ")");
        isOn = active;
    }

    protected override void Interact()
    {
        // Unplug if plugged in
        if (isOn)
        {
            onPoweredOff.Invoke();
            isOn = true;
        }

        // Plug in if unplugged
        else
        {
            onPoweredOn.Invoke();
            isOn = false;
        }
    }
}