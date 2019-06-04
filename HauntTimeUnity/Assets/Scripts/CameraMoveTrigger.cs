using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMoveTrigger : MonoBehaviour
{
    public CameraController cameraController;

    //event to let us know it's time to snap cam back to Hatto
    public UnityEvent offPlayerDone;
    //Event to stop hatto
    public UnityEvent freezeHatto;
    //list of rooms, must be ordered left to right
    public List<Transform> rooms = new List<Transform>();
    public bool startOnLeft;
    //room index of hatto's room
    public int hattoRoom;
    //room index of where camera is currently
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
    //switch cam to hatto, coroutined to delay touch and move input to accomadate camera time
    public void SwitchToHatto()
    {
        cameraController.MoveCamera(rooms[hattoRoom]);
        currentCamRoom = hattoRoom;
        StartCoroutine(DelayedHattoMove());
    }
    //track and interactable obj to the left, freeze hatto, move cam, index
    public void TrackInteractableLeft()
    {
        //Debug.Log("Moving to left room");
        freezeHatto.Invoke();
        cameraController.MoveCamera(rooms[currentCamRoom - 1]);
        currentCamRoom--;
    }
    //same as above, but to the right
    public void TrackInteractableRight()
    {
        Debug.Log("Moving to right room");
        freezeHatto.Invoke();
        cameraController.MoveCamera(rooms[currentCamRoom + 1]);
        currentCamRoom++;
    }
    //move camera to the left with hatto
    public void TrackHattoLeft()
    {
        Debug.Log("Moving to left room");
        cameraController.MoveCamera(rooms[hattoRoom - 1]);
        hattoRoom--;
        currentCamRoom = hattoRoom;
    }
    //move camera to the right with hatto
    public void TrackHattoRight()
    {
        Debug.Log("Moving to right room");
        cameraController.MoveCamera(rooms[hattoRoom + 1]);
        hattoRoom++;
        currentCamRoom = hattoRoom;
    }
    //delay hatto control for a second to let camera pan without user sending hatto to a new room inadvertantly.
    IEnumerator DelayedHattoMove()
    {
        yield return new WaitForSeconds(.5f);
        offPlayerDone.Invoke();
    }
}
