﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class miniCamTrigger : MonoBehaviour
{
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
        Debug.Log(other.gameObject.tag + " passed thru the room-change collider");
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
        else if (other.gameObject.CompareTag("Interactable"))
        {
            Vector2 collisionPoint = other.gameObject.transform.position;

            // Collision from right
            if (collisionPoint.x > transform.position.x)
            {
                // Switch to left room
                interactableLeft.Invoke();

                // TODO: move player fully past trigger
            }

            // Collision from left
            else if (collisionPoint.x < transform.position.x)
            {
                // Switch to right room
                interactableRight.Invoke();

                // TODO: move player fully past trigger
            }
        }
    }
}