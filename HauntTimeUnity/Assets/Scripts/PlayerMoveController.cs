using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 movement = Vector2.zero;
        if(Input.GetKey(KeyCode.W)){
            movement += Vector2.up * speed;
        }
        else if(Input.GetKey(KeyCode.S)){
            movement += Vector2.down * speed;
        }
        if(Input.GetKey(KeyCode.A)){
            movement += Vector2.left * speed;
        }
        else if(Input.GetKey(KeyCode.D)){
            movement += Vector2.right * speed;
        }

        rb.MovePosition((Vector2)transform.position + movement);
    }
}
