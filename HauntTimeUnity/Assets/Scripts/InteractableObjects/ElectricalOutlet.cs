using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElectricalOutlet : InteractableObject
{
    public bool pluggedIn;

    /// <summary>
    /// Electronic plugged into outlet
    /// </summary>
    public Electronic electronic;

    /// <summary>
    /// Event called when unplugged
    /// </summary>
    public UnityEvent onUnplugged;

    /// <summary>
    /// Event called when plugged in
    /// </summary>
    public UnityEvent onPlugged;
    
    protected override void Start()
    {
        base.Start();
        //inOutletCollider = false;
        pluggedIn = true;

        // Add default listeners to plug events
        onUnplugged.AddListener(Unplug);
        onPlugged.AddListener(PlugIn);
        
        // Add listeners to electronic if assigned
        if(electronic != null) {
            onUnplugged.AddListener(() => electronic.SetActive(false));
            onPlugged.AddListener(() => electronic.SetActive(true));
        }
    }

    protected override void Interact()
    {
        // Unplug if plugged in
        if (pluggedIn) {
            onUnplugged.Invoke();
        }

        // Plug in if unplugged
        else {
            onPlugged.Invoke();
        }
    }

    //a function to unplug, simply change boolean. Will add animation here when ready.
    public void Unplug()
    {
        Debug.Log("Unplugged the outlet");
        pluggedIn = false;
    }
    //opposite of above
    public void PlugIn()
    {
        Debug.Log("plugged the outlet in");
        pluggedIn = true;
    }
}
