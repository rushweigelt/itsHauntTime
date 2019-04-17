using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public float horizontalSpeed;
    public float verticalSpeed;

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

            // Face left
            spriteRenderer.flipX = false;
            facingDirection = Direction.LEFT;
        }
        // Right
        else if(Input.GetKey(KeyCode.D)){
            movement += Vector2.right * horizontalSpeed;

            // Face right
            spriteRenderer.flipX = true;
            facingDirection = Direction.RIGHT;
        }

        rb.MovePosition((Vector2)transform.position + movement);
    }
}
