using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTrigger : MonoBehaviour
{
    public CameraController cameraController;

    //public GameObject pipOb;

    //public CameraController ccPip;

    //public Transform leftCameraViewpoint;

    //public Transform rightCameraViewpoint;

    //public Transform MiddleCameraViewpoint;

    public List<Transform> rooms = new List<Transform>();
    public bool startOnLeft;
    int currentRoom;
    int currentPipRoom;


    public void Start()
    {
        //pipOb.SetActive(false);
        if (startOnLeft)
        {
            currentRoom = 0;
            currentPipRoom = 0;
        }
        else
        {
            currentRoom = 1;
            currentPipRoom = 1;
        }
        
    }


    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag + " passed thru the room-change collider");
        if (other.gameObject.CompareTag("Player")) {
            Vector2 collisionPoint = other.gameObject.transform.position;

            // Collision from right
            if (collisionPoint.x > transform.position.x) {
                // Switch to left room
                Debug.Log("Moving to left room");
                cameraController.MoveCamera(rooms[currentRoom - 1]);
                //ccPip.MoveCamera(rooms[currentRoom - 1]);
                currentRoom--;

                // TODO: move player fully past trigger
            }

            // Collision from left
            else if (collisionPoint.x < transform.position.x) {
                // Switch to right room
                Debug.Log("Moving to right room");
                cameraController.MoveCamera(rooms[currentRoom + 1]);
                //ccPip.MoveCamera(rooms[currentRoom + 1]);
                currentRoom++;

                // TODO: move player fully past trigger
            }
        }
        /*
        else if(other.gameObject.CompareTag("Interactable"))
        {
            pipOb.SetActive(true);
            Vector2 collisionPoint = other.gameObject.transform.position;

            // Collision from right
            if (collisionPoint.x > transform.position.x)
            {
                // Switch to left room
                Debug.Log("Moving to left room");
                ccPip.MoveCamera(rooms[currentPipRoom - 1]);
                currentPipRoom--;

                // TODO: move player fully past trigger
            }

            // Collision from left
            else if (collisionPoint.x < transform.position.x)
            {
                // Switch to right room
                Debug.Log("Moving to right room");
                ccPip.MoveCamera(rooms[currentPipRoom + 1]);
                currentPipRoom++;

                // TODO: move player fully past trigger
            }
        }
        */
    }
    /*
    public void ShutPipOff()
    {
        StartCoroutine(StopPip());
    }

    IEnumerator StopPip()
    {
        yield return new WaitForSeconds(2);
        pipOb.SetActive(false);
    }
    */
}
