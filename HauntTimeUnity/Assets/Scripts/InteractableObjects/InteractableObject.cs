using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    bool inRange;

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
        if(inRange) {
            // TODO: replace mouse detection with touch detection before building to mobile
            if(Input.GetMouseButtonDown(0)) {
                Vector2 touchPos = Input.mousePosition;
                if(CheckTouch(touchPos) && canInteract) {
                    // Call onInteract listeners
                    onInteract.Invoke();

                    // Call derived Interact() method
                    Interact();
                }
            }
        }
    }

    /// <summary>
    /// Returns true if touch hit collider, false otherwise.
    /// Object must be on Interactable layer to receive raycasts.
    /// </summary>
    /// <param name="touchPos">Position of player touch</param>
    private bool CheckTouch(Vector2 touchPos)
    {
        // Return false with warning if not tagged properly
        if(!gameObject.CompareTag("Interactable")) {
            Debug.LogWarningFormat("Warning: {0} not tagged as Interactable - will not receive touch input", name);
            return false;
        }

        // Get touch position in world space
        Vector2 touchPosWorld = Camera.main.ScreenToWorldPoint(touchPos);

        // Check for raycast collisions in Interacatable layer only
        int layermask = 1 << LayerMask.NameToLayer("Interactable");
        RaycastHit2D raycast = Physics2D.Raycast(touchPosWorld, Camera.main.transform.forward, Mathf.Infinity, layermask);

        // Return true if raycast hit collider
        return raycast.collider == interactRange;
    }

    /// <summary>
    /// Must be overridden by derived class
    /// </summary>
    protected virtual void Interact() {}

    /// <summary>
    /// Event called on interaction
    /// </summary>
    public UnityEvent onInteract;


    /// <summary>
    /// Called when player enters interact range
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // Enable inRange and interact prompt
            inRange = true;
            interactPrompt.SetActive(true);

            // Set player to transparent
            Player.Instance.SetTransparent(true);
        }
    }
    
    /// <summary>
    /// Called when player leaves interact range
    /// </summary>
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // Disable inRange and interact prompt
            inRange = false;
            interactPrompt.SetActive(false);

            // Set player to opaque
            Player.Instance.SetTransparent(false);
        }
    }
}
