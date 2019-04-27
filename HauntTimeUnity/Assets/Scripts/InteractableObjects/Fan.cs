using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Electronic
{
    public BoxCollider2D fanRange;//game object that is a collider, used to prevent the player from advancing, simulating push back
    public ElectricalOutlet eo; //bind an electronic to it's outlet

    // Start is called before the first frame update
    protected override void Start()
    {
        fanRange.enabled = true;
    }

    // Update is called once per frame. We use update, not fixed update, as fixed update interferes with Electronic.
    protected override void Update()
    {
        CheckPowerStatus();
    }
    //the specific outcome of the electronic-level component being toggled;
    //here, we toggle a box collider on and off that represents a fan preventing 
    //the ghost from moving forward.
    void FanOn()
    {
        fanRange.enabled = true;        
    }

    void FanOff()
    {
        fanRange.enabled = false;
    }
    //a function that will run constantly, checking the status of fan by 
    //looking at the electronic-level subclass.
    void CheckPowerStatus()
    {
        if (eo.pluggedIn == false)
        {
            FanOff();
        }
        if (eo.pluggedIn == true)
        {
            FanOn();
        }
    }
}
