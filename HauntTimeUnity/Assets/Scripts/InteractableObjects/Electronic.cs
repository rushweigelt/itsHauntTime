using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electronic : InteractableObject
{
    public bool isOn = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
