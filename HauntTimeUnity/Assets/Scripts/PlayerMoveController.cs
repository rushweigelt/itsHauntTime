using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    void Update()
    {
        // Get input (position of last touch)
        Vector2 input = touchInput.GetInput();

        // Handle movement input if movement unlocked
        MoveTowards(input);

        // Handle animation based on touch input scheme
        if(touchInput.controlScheme.Equals(TouchInput.ControlScheme.FOLLOW_TAP)) {
            Animate(input - (Vector2)transform.position);
        }
        else {
            Animate(input);
        }
    }

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
    public void MoveTowards(Vector2 target)
    {
        if(canMove) {
            // Get vector towards target
            Vector2 delta = target - (Vector2)transform.position;

            // Ignore if not long enough (to avoid jittering around target)
            if (delta.magnitude > touchInput.touchThreshold) {
                Move(delta);
            }
        }
    }

    /// <summary>
    /// Move in direction of input
    /// </summary>
    /// <param name="input"></param>
    private void Move(Vector2 input)
    {
        // Normalize input and scale by move speed
        Vector2 movement = input.normalized;
        movement.x *= horizontalSpeed;
        movement.y *= verticalSpeed;
        
        rb.MovePosition((Vector2)transform.position + movement);
    }
}
