using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{

    Rigidbody2D rb;
    public float horizontalSpeed;
    public float verticalSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 movement = Vector2.zero;
        if(Input.GetKey(KeyCode.W)){
            movement += Vector2.up * verticalSpeed;
        }
        else if(Input.GetKey(KeyCode.S)){
            movement += Vector2.down * verticalSpeed;
        }
        if(Input.GetKey(KeyCode.A)){
            movement += Vector2.left * horizontalSpeed;
        }
        else if(Input.GetKey(KeyCode.D)){
            movement += Vector2.right * horizontalSpeed;
        }

        rb.MovePosition((Vector2)transform.position + movement);
    }
}
