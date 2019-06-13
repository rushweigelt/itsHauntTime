using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TouchInput), typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class PlayerMoveController : Singleton<PlayerMoveController>
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    TouchInput touchInput;

    public float horizontalSpeed;
    public float verticalSpeed;

    /// <summary>
    /// Set true to enable player movement or false to lock it
    /// </summary>
    public bool canMove = true;

    Coroutine movementCoroutine;


    enum Direction {
        LEFT,
        RIGHT
    }
    Direction facingDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        touchInput = GetComponent<TouchInput>();

        // HandleMovement() and Animate() methods subscribe to first frame of touch input
        touchInput.onGetTouchDown += (input) => MoveTowards(input);
        //touchInput.onGetTouchDown += (input) => FaceDirection(input - (Vector2)transform.position);

        Debug.Log("World Position" + transform.position);
        Debug.Log("Local Position" + transform.localPosition);
    }

    /// <summary>
    /// Starts movement coroutine towards user tap
    /// </summary>
    /// <param name="position"></param>
    public void MoveTowards(Vector2 position)
    {
        // Face towards target position
        Vector2 delta = position - (Vector2)transform.position;
        if(delta.x < 0) {
            FaceDirection(Direction.LEFT);
        }
        else {
            FaceDirection(Direction.RIGHT);
        }

        // Check if we're moving to interact with something
        InteractableObject interactable = InteractableObject.GetInteractableAt(position);
        if(interactable != null && interactable.canInteract) {
            Debug.Log("PlayerMoveController - moving towards interactable " + interactable.name);

            position = FindPositionNear(interactable);
        }

        // Interrupt movement if coroutine already active
        if(movementCoroutine != null) {
            //Debug.Log("Interrupting movement coroutine");
            StopCoroutine(movementCoroutine);
        }
        movementCoroutine = StartCoroutine(MoveTowardsCoroutine(position));
    }

    private Vector2 FindPositionNear(InteractableObject interactable)
    {
        // Find a convenient position next to interactable (left/right side)
        float distanceFromInteractable = interactable.interactDistance;
        Vector2 pos = interactable.transform.position;

        Vector3 toLeft = new Vector3(pos.x - distanceFromInteractable, pos.y);
        Vector3 toRight = new Vector3(pos.x + distanceFromInteractable, pos.y);
        Vector2 nearest = (toLeft - transform.position).magnitude < (toRight - transform.position).magnitude ? toLeft : toRight;

        return nearest;
    }

    /// <summary>
    /// SpriteRenderer faces direction of user tap
    /// </summary>
    /// <param name="input"></param>
    private void FaceDirection(Direction direction)
    {
        // Flip spriterenderer.x accordingly
        spriteRenderer.flipX = direction.Equals(Direction.RIGHT);
        facingDirection = direction;
    }

    /// <summary>
    /// Move towards position at fixed speed
    /// </summary>
    /// <param name="target"></param>
    private IEnumerator MoveTowardsCoroutine(Vector2 target)
    {
        if(canMove) {
            // Get vector towards target
            Vector2 delta = target - (Vector2)transform.position;

            // Continue moving towards target until close enough to avoid jittering (determined by touchThreshold)
            while (delta.magnitude > touchInput.touchThreshold) {
                // Normalize input and scale by move speed
                Vector2 movement = delta.normalized;
                movement.x *= horizontalSpeed;
                movement.y *= verticalSpeed;

                // Move towards target
                rb.MovePosition((Vector2)transform.position + movement);

                // Update our movement vector and wait a frame
                delta = target - (Vector2)transform.position;
                yield return null;
            }
        }
        // Set coroutine to null so we know it's not active (does this happen automatically after it's finished?)
        movementCoroutine = null;
    }

    public void MoveOn()
    {
        canMove = true;
        touchInput.enabled = true;
    }

    public void MoveOff()
    {
        canMove = false;
        touchInput.enabled = false;
    }
}
