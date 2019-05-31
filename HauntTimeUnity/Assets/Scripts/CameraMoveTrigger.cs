using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMoveTrigger : MonoBehaviour
{
    public CameraController cameraController;

    //event to 
    public UnityEvent offPlayerDone;

    public UnityEvent freezeHatto;

    public List<Transform> rooms = new List<Transform>();
    public bool startOnLeft;
    public int hattoRoom;
    public int currentCamRoom;


    public void Start()
    {
        if (startOnLeft)
        {
            hattoRoom = 0;
            currentCamRoom = 0;
        }
        else
        {
            hattoRoom = 1;
            currentCamRoom = 1;
        }

    }

    /*
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag + " passed thru the room-change collider");
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 collisionPoint = other.gameObject.transform.position;

            // Collision from right
            if (collisionPoint.x > transform.position.x)
            {
                // Switch to left room
                Debug.Log("Moving to left room");
                cameraController.MoveCamera(rooms[hattoRoom - 1]);
                hattoRoom--;
                //hattoRoom = currentCamRoom;

                // TODO: move player fully past trigger
            }

            // Collision from left
            else if (collisionPoint.x < transform.position.x)
            {
                // Switch to right room
                Debug.Log("Moving to right room");
                cameraController.MoveCamera(rooms[hattoRoom + 1]);
                hattoRoom++;
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
                Debug.Log("Moving to left room");
                cameraController.MoveCamera(rooms[currentCamRoom - 1]);
                currentCamRoom--;

                // TODO: move player fully past trigger
            }

            // Collision from left
            else if (collisionPoint.x < transform.position.x)
            {
                // Switch to right room
                Debug.Log("Moving to right room");
                cameraController.MoveCamera(rooms[currentCamRoom + 1]);
                currentCamRoom++;

                // TODO: move player fully past trigger
            }
        }
    }
    */

    public void SwitchToHatto()
    {
        cameraController.MoveCamera(rooms[hattoRoom]);
        currentCamRoom = hattoRoom;
        StartCoroutine(DelayedHattoMove());
    }

    public void TrackInteractableLeft()
    {
        Debug.Log("Moving to left room");
        freezeHatto.Invoke();
        cameraController.MoveCamera(rooms[currentCamRoom - 1]);
        currentCamRoom--;
    }

    public void TrackInteractableRight()
    {
        Debug.Log("Moving to right room");
        freezeHatto.Invoke();
        cameraController.MoveCamera(rooms[currentCamRoom + 1]);
        currentCamRoom++;
    }

    public void TrackHattoLeft()
    {
        Debug.Log("Moving to left room");
        cameraController.MoveCamera(rooms[hattoRoom - 1]);
        hattoRoom--;
        currentCamRoom = hattoRoom;
    }

    public void TrackHattoRight()
    {
        Debug.Log("Moving to right room");
        cameraController.MoveCamera(rooms[hattoRoom + 1]);
        hattoRoom++;
        currentCamRoom = hattoRoom;
    }

    IEnumerator DelayedHattoMove()
    {
        yield return new WaitForSeconds(.5f);
        offPlayerDone.Invoke();
    }
}
