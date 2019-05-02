using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareTarget : InteractableObject
{
    public string state; //used to communicate states between objects, change to enum

    // Start is called before the first frame update
    protected override void Start()
    {
        //inRange = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected void InitialState()
    {

    }

    protected void ScaredState()
    {

    }

    protected void AlertedState()
    {

    }

    protected override void Interact()
    {
        if (state == "Scareable")
        {
            Debug.Log("Scare Target was Scared");
            state = "Scared";
        }
        else
        {
            Debug.Log("Tried to interact, but target is not yet scareable!");
        }
    }
}

