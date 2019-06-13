using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class miniCamTrigger : MonoBehaviour
{
    //events to tell our Trigger Control (parent obj) which way to pan.
    public UnityEvent hattoLeft;
    public UnityEvent hattoRight;
    public UnityEvent interactableLeft;
    public UnityEvent interactableRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.tag + " passed thru the room-change collider");
        //if it's the player and we're going to the left, invoke accordingly.
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 collisionPoint = other.gameObject.transform.position;

            // Collision from right
            if (collisionPoint.x > transform.position.x)
            {
                //Hatto is moving left
                hattoLeft.Invoke();

                // TODO: move player fully past trigger
            }

            // Collision from left
            else if (collisionPoint.x < transform.position.x)
            {
                // Switch to right room
                hattoRight.Invoke();
                //hattoRoom = currentCamRoom;

                // TODO: move player fully past trigger
            }
        }
        //if the object is tagged as an interactaable, freeze Hatto(via invoke) and pan accordingly. Obj must have rigidbody.
        else if (other.gameObject.CompareTag("Interactable"))
        {
            // Don't pan camera for player-held object
            InteractableObject interactable = other.GetComponent<InteractableObject>();
            if(interactable == Player.Instance.heldObject) {
                Debug.Log("CameraTrigger: Ignoring player held object " + other.name);
            }
            
            else {
                Vector2 collisionPoint = other.gameObject.transform.position;

                // Collision from right
                if (collisionPoint.x > transform.position.x)
                {
                    Debug.Log("Interact Col Point: " + collisionPoint.x + " camera position: " + transform.position.x);
                    // Switch to left room
                    interactableLeft.Invoke();

                    // TODO: move player fully past trigger
                }

                // Collision from left
                else if (collisionPoint.x < transform.position.x)
                {
                    Debug.Log("Interact Col Point: " + collisionPoint.x + " camera position: " + transform.position.x);
                    // Switch to right room
                    interactableRight.Invoke();

                    // TODO: move player fully past trigger
                }
            }
        }
    }
}
