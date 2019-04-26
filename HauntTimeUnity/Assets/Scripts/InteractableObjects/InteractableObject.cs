using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    /// <summary>
    /// Zone within which the player can interact with this object
    /// </summary>
    public BoxCollider2D interactRange;

    /// <summary>
    /// Popup indicator that player can interact with this object
    /// </summary>
    public GameObject interactPrompt;

    /// <summary>
    /// True if player is within interactRange
    /// </summary>
    public bool inRange;

    /// <summary>
    /// True if the player can interact with this object
    /// </summary>
    public bool canInteract = true;

    // Start is called before the first frame update. Leave empty, to not interfere with initializing subclasses
    protected virtual void Start()
    {
   
    }
    // Update is called once per frame. Again, leave empty to avoid interfering with subclass update functions
    protected virtual void Update()
    {
        if(canInteract && inRange) {
            if(Input.GetKeyDown(KeyCode.E)) {
                Debug.Log(name + ".Interact()");
                Interact();
            }
        }
    }

    /// <summary>
    /// Must be overridden by derived class
    /// </summary>
    protected virtual void Interact() {}
    
    //if the player nears an interactable object, a thought bubble appears above the players head.
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inRange = true;
            interactPrompt.SetActive(true);
        }
    }
    //if the player leaves the range, disable thought bubble
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inRange = false;
            interactPrompt.SetActive(false);
        }
    }
}
