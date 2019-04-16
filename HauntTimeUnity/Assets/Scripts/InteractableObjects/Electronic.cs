using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electronic : InteractableObject
{
    public bool isOn;
    public ElectricalOutlet outlet; 

    // Start is called before the first frame update
    protected override void Start()
    {
        isOn = true;
    }

    //Since this is a mid-class, we are going to use FixedUpdate here, that way we can use Update in the lowest class.
    protected void FixedUpdate()
    {
        //check any electronic to see whether it is plugged in or not
        CheckOutlet();
    }

    //the most skeleton of an electronic, this simply toggles it on and off via checking if it's plugged in. 
    //More complex, specific code will be used in a class-down. See Fan for an example
    public void TurnOn()
    {
        if (isOn == false)
        {
            isOn = true;
        }
    }

    public void TurnOff()
    {
        if (isOn == true)
        {
            isOn = false;
        }
    }
    //a function to check on any electronic's electricity status via checking the outlet.
    //Will be run constantly via update.
    public void CheckOutlet()
    {
        if (outlet.pluggedIn == false)
        {
            TurnOff();
        }
        else if (outlet.pluggedIn == true)
        {
            TurnOn();
        }
    }
}
