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

    //the most skeleton of an electronic, this simply toggles it on and off
    //More complex, specific code will be used in a class-down. See Fan for an example
    public virtual void SetActive(bool active) 
    {
        isOn = active;
    }
}
