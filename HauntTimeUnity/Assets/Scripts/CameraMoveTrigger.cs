﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTrigger : MonoBehaviour
{

    public Transform leftCameraViewpoint;

    public Transform rightCameraViewpoint;

    
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 collisionPoint = other.gameObject.transform.position;

        // Collision from right
        if(collisionPoint.x > transform.position.x) {
            // Switch to left room
            CameraController.MoveCamera(leftCameraViewpoint);
        }

        // Collision from left
        else if(collisionPoint.x < transform.position.x) {
            // Switch to right room
            CameraController.MoveCamera(rightCameraViewpoint);
        }
    }
}
