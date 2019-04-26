using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public BoxCollider2D interactRange; //a box collider that represents the range at which the player can interact with the object.
    public GameObject interactPrompt; //visual popup
    public bool inRange;
    // Start is called before the first frame update. Leave empty, to not interfere with initializing subclasses
    protected virtual void Start()
    {
   
    }
    // Update is called once per frame. Again, leave empty to avoid interfering with subclass update functions
    protected virtual void Update()
    {
        
    }
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
