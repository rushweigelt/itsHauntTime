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
    //event to stop timer
    //public UnityEvent freezeTimer;
    //event to start timer
    //public UnityEvent restartTimer;
    //a float to control how long we pause on a room during intro sequence
    public float waitTime;
    //float for countdown timer to unfreeze hatto and start level
    public float countdown;
    //bool to turn on dev mode, bc we don't wanna sit through the intro every time.
    public bool devMode;
    //intro gameob to toggle
    public GameObject introPrompt;


    public void Start()
    {
        introPrompt.SetActive(false);
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
        if (devMode)
        {

        }
        else
        {
            IntroPan();
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
        yield return new WaitForSeconds(cameraController.panSpeed-.5f);
        offPlayerDone.Invoke();
    }

    public void IntroPan()
    {
        freezeHatto.Invoke();
        StartCoroutine(IntroCamera());        
    }
    //get size of room list, use for loop and wait for seconds to pause, switch back to hatto when complete.
    IEnumerator IntroCamera()
    {
        int size = rooms.Count;
        Debug.Log("size: " + size.ToString());

        yield return new WaitForSeconds(waitTime-(waitTime*.5f));
        //minus 1 because we don't want to go past last room
        for (int i = 0; i < size-1; i++)
        {
            cameraController.MoveCamera(rooms[currentCamRoom + 1]);
            currentCamRoom++;
            yield return new WaitForSeconds(waitTime);
        }
        //switch to Hatto
        cameraController.MoveCamera(rooms[hattoRoom]);
        currentCamRoom = hattoRoom;
        //wait for camera to be on hatto before activating prompt
        yield return new WaitForSeconds(cameraController.panSpeed - .7f);
        introPrompt.SetActive(true);
        //deactivate prompt after a few seconds
        yield return new WaitForSeconds(countdown);
        introPrompt.SetActive(false);
        //unlock hatto and start timer
        offPlayerDone.Invoke();
    }
}
