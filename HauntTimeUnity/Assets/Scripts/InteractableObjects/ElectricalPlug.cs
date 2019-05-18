using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElectricalPlug : SpriteSwapInteraction
{
    public bool pluggedIn;

    /// <summary>
    /// Electronic plugged into outlet
    /// </summary>
    public Electronic electronic;
    
    protected override void Start()
    {
        base.Start();
        //inOutletCollider = false;
        pluggedIn = true;
    }

    protected override void Interact()
    {
        base.Interact();
        // Unplug if plugged in
        if (pluggedIn) {
            electronic.onPoweredOff.Invoke();
            Unplug();
        }

        // Plug in if unplugged
        else {
            electronic.onPoweredOn.Invoke();
            PlugIn();
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
