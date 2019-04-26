using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalOutlet : InteractableObject
{
    public bool pluggedIn; //is the power plug in the outlet?
    public bool inOutletCollider; //is the player in range of the outlet

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        inOutletCollider = false;
        pluggedIn = true;
    }

    protected override void Interact()
    {
        if (pluggedIn == true && inOutletCollider == true)
        {
            Unplug();
        }
        else if (pluggedIn == false && inOutletCollider == true)
        {
            PlugIn();
        }
    }

    //a function to unplug, simply change boolean. Will add animation here when ready.
    public void Unplug()
    {
        if (pluggedIn == true)
        {
            Debug.Log("Unplugged the outlet");
            pluggedIn = false;
        }
    }
    //opposite of above
    public void PlugIn()
    {
        if (pluggedIn == false)
        {
            Debug.Log("plugged the outlet in");
            pluggedIn = true;
        }
    }
    //detect if player is near the outlet collider; all electronics will require an outlet
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inOutletCollider = true;
        }
    }
    //when we leave outlet collider, inverse previous func.
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inOutletCollider = false;
        }
    }
}
