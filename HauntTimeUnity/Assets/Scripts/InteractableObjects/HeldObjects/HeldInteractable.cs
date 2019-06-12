using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldInteractable : InteractableObject
{
    /// <summary>
    /// Sound played when used
    /// </summary>
    public SoundController.SoundType usedSound;
    
    [Header("Held Object Settings")]
    public List<InteractableObject> canBeUsedOn;

    /// <summary>
    /// Player picks up and holds object
    /// </summary>
    protected override void Interact()
    {
        // Player picks up object if not already holding one
        if(!Player.Instance.IsHoldingObject()) {
            Player.Instance.PickUp(this);

            // Set interactable to false so player doesn't accidentally tap it again
            canInteract = false;
        }
        else {
            Debug.Log("Can't pick up " + name + ": already holding " + Player.Instance.heldObject.name);
        }
    }

    public bool CanBeUsedOn(InteractableObject obj) {
        return canBeUsedOn.Contains(obj);
    }


    /// <summary>
    /// Called to use object while held
    /// </summary>
    public virtual void UseOn(InteractableObject obj) {
        Debug.Log(name + " .Use()");
    }

    public virtual void Drop()
    {
        Debug.Log("Dropping " + name);
        Destroy(gameObject);
    }
}
