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
        touchInput.onGetTouchDown += (input) => HandleMovement(input);
        touchInput.onGetTouchDown += (input) => Animate(input - (Vector2)transform.position);
    }

    /// <summary>
    /// Starts movement coroutine towards user tap
    /// </summary>
    /// <param name="input"></param>
    void HandleMovement(Vector2 input)
    {
        // Interrupt movement if coroutine already active
        if(movementCoroutine != null) {
            Debug.Log("Interrupting movement coroutine");
            StopCoroutine(movementCoroutine);
        }
        movementCoroutine = StartCoroutine(MoveTowards(input));
    }

    /// <summary>
    /// SpriteRenderer faces direction of user tap
    /// </summary>
    /// <param name="input"></param>
    private void Animate(Vector2 input)
    {
        // Face left
        if(input.x < 0) {
            spriteRenderer.flipX = false;
            facingDirection = Direction.LEFT;
        }
        // Face right
        else if(input.x > 0) {
            spriteRenderer.flipX = true;
            facingDirection = Direction.RIGHT;
        }
    }

    /// <summary>
    /// Move towards position at fixed speed
    /// </summary>
    /// <param name="target"></param>
    public IEnumerator MoveTowards(Vector2 target)
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
