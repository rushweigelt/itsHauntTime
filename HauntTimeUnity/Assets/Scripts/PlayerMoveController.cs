using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : Singleton<PlayerMoveController>
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public float horizontalSpeed;
    public float verticalSpeed;

    public TouchInput touchInput;

    enum Direction {
        LEFT,
        RIGHT
    }

    Direction facingDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get input (position of last touch)
        Vector2 input = touchInput.GetInput();
        
        MoveTowards(input);

        if(touchInput.controlScheme.Equals(TouchInput.ControlScheme.FOLLOW_TAP))
        {
            Animate(input - (Vector2)transform.position);
        }
        else
        {
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
        // Get vector towards target
        Vector2 delta = target - (Vector2)transform.position;

        // Ignore if not long enough (to avoid jittering around target)
        if(delta.magnitude > touchInput.touchThreshold) {
            Move(delta);
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

    Vector2 GetKeyboardInput()
    {
        Vector2 movement = Vector2.zero;

        // Up
        if(Input.GetKey(KeyCode.W)){
            movement += Vector2.up * verticalSpeed;
        }
        // Down
        else if(Input.GetKey(KeyCode.S)){
            movement += Vector2.down * verticalSpeed;
        }

        // Left
        if(Input.GetKey(KeyCode.A)){
            movement += Vector2.left * horizontalSpeed;
        }
        // Right
        else if(Input.GetKey(KeyCode.D)){
            movement += Vector2.right * horizontalSpeed;
        }

        return movement;
    }
}
