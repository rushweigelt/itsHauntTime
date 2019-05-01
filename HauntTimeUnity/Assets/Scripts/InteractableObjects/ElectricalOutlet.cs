using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElectricalOutlet : InteractableObject
{
    public bool pluggedIn;

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
    }

    protected override void Interact()
    {
        if (pluggedIn) {
            onUnplugged.Invoke();
        }
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
    /*
    //detect if player is near the outlet collider; all electronics will require an outlet
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    //when we leave outlet collider, inverse previous func.
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
    */
}
