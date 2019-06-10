using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldInteractable : InteractableObject
{
    /// <summary>
    /// Player picks up and holds object
    /// </summary>
    protected override void Interact()
    {
        // Pick up object before 
        if(!Player.Instance.IsHoldingObject()) {
            Player.Instance.PickUp(this);

            // Set interactable to false so player doesn't accidentally tap it again
            canInteract = false;
        }
    }


    /// <summary>
    /// Player uses this object
    /// </summary>
    protected virtual void Use() {
        Debug.Log(name + " .Use()");
    }
}
