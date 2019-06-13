using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class InteractableObject : MonoBehaviour
{
    [Header("Interactable Object Settings")]
    /// <summary>
    /// True if the player can interact with this object
    /// </summary>
    public bool canInteract = true;

    /// <summary>
    /// Zone within which the player can interact with this object
    /// </summary>
    public BoxCollider2D interactRange;

    /// <summary>
    /// Distance from which the player should stop before interacting
    /// </summary>
    public float interactDistance;

    /// <summary>
    /// Popup indicator that player can interact with this object
    /// </summary>
    public GameObject interactPrompt;

    /// <summary>
    /// True if player is within interactRange
    /// </summary>
    bool inRange;

    [Header("Sound Effect Settings")]

    public bool playSound = false;
    public SoundController.SoundType interactSound;

    UnityEvent onPlayerEnterTrigger = new UnityEvent();

    protected virtual void Start()
    {
        // TODO: remove this start() method - it's cumbersome/risky for devs to remember to call this base method
    }
    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Reset listeners on click
            onPlayerEnterTrigger.RemoveAllListeners();

            // Get touch position (TODO: use TouchInput)
            Vector2 touchPos = Input.mousePosition;
            if (CheckTouch(touchPos, this) && canInteract) {
                // Interact immediately if already in range
                if(inRange) {
                    // interactListener.Invoke();
                    InteractHandler();
                }
                // Otherwise wait until we are in range to interact
                else
                {
                    Debug.Log(name + " | Adding Interact() listener");
                    // onPlayerEnterTrigger.AddListener(interactListener);
                    onPlayerEnterTrigger.AddListener(() => InteractHandler());
                }
            }
        }
    }

    private void InteractHandler()
    {
        Debug.Log(name + ".Interact()");

        // Call onInteract listeners
        onInteract.Invoke();

        // Play sound if needed
        if(playSound) {
            SoundController.Instance.PlaySoundEffect(interactSound);
        }
        Player player = Player.Instance;

        // Play interact animation
        player.animController.SetTrigger(PlayerAnimController.AnimState.INTERACT);
        
        if(player.IsHoldingObject() && player.heldObject.CanBeUsedOn(this)) {
            Debug.Log("Using " + player.heldObject.name + " on " + name);
            player.heldObject.UseOn(this);
        }
        else {
            // Call derived Interact() method
            Interact();
        }
    }

    /// <summary>
    /// Returns true if user tapped the InteractableObject, false otherwise.
    /// Object must be on Interactable layer and tagged as Interactable to receive raycasts.
    /// </summary>
    /// <param name="touchPos">Position of player touch</param>
    public static bool CheckTouch(Vector2 touchPos, InteractableObject interactable)
    {
        // Return false with warning if not tagged properly
        if(!interactable.gameObject.CompareTag("Interactable")) {
            Debug.LogWarningFormat("Warning: {0} not tagged as Interactable - will not receive touch input", interactable.name);
            return false;
        }

        // Convert touch position to world space (TODO: remove this when using world-space input from TouchInput)
        Vector2 touchPosWorld = Camera.main.ScreenToWorldPoint(touchPos);
        return GetInteractableAt(touchPosWorld) == interactable;
    }

    /// <summary>
    /// Returns the InteractableObject located at position of touch, or null if none found
    /// </summary>
    /// <param name="touchPos">Position of user touch</param>
    public static InteractableObject GetInteractableAt(Vector2 touchPos)
    {
        // Check for raycast collisions (NOTE: only detects objects in Interacatable layer & tagged as Interactable)
        int layermask = 1 << LayerMask.NameToLayer("Interactable");
        RaycastHit2D raycast = Physics2D.Raycast(touchPos, Camera.main.transform.forward, Mathf.Infinity, layermask);
        if(raycast.collider != null) {
            GameObject obj = raycast.collider.gameObject;
            return obj.GetComponent<InteractableObject>();
        }
        return null;
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
            // Invoke listeners, then remove them
            onPlayerEnterTrigger.Invoke();
            onPlayerEnterTrigger.RemoveAllListeners();

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

