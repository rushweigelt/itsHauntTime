using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTrigger : MonoBehaviour
{
    public CameraController cameraController;

    //public Transform leftCameraViewpoint;

    //public Transform rightCameraViewpoint;

    //public Transform MiddleCameraViewpoint;

    public List<Transform> rooms = new List<Transform>();

    int currentRoom;

    public void Start()
    {
        currentRoom = 0;
    }


    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") {
            Vector2 collisionPoint = other.gameObject.transform.position;

            // Collision from right
            if(collisionPoint.x > transform.position.x) {
                // Switch to left room
                Debug.Log("Moving to left room");
                cameraController.MoveCamera(rooms[currentRoom-1]);
                currentRoom -= 1;

                // TODO: move player fully past trigger
            }

            // Collision from left
            else if(collisionPoint.x < transform.position.x) {
                // Switch to right room
                Debug.Log("Moving to right room");
                cameraController.MoveCamera(rooms[currentRoom+1]);
                currentRoom += 1;

                // TODO: move player fully past trigger
            }
        }
    }
}
