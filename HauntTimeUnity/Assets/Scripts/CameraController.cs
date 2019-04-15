using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum TransitionMode {
        SMOOTH,
        SNAP
    };
    public TransitionMode mode;

    /// <summary>
    /// Speed of smooth camera transition
    /// </summary>
    [Range(.5f, 2)]
    public float panSpeed = 1.5f;

    /// <summary>
    /// Moves camera from current position to the specified room
    /// </summary>
    /// <param name="room">Room viewpoint to move camera to</param>
    public void MoveCamera(Transform room)
    {
        switch(mode) {
            case TransitionMode.SMOOTH:
                StartCoroutine(PanCamera(Camera.main.transform.position, room.position));
                break;
            case TransitionMode.SNAP:
                Camera.main.transform.position = room.position;
                break;
        }
    }

    /// <summary>
    /// Performs smooth camera transition between rooms
    /// </summary>
    /// <param name="start">Starting camera position</param>
    /// <param name="end">Ending camera position</param>
    /// <returns></returns>
    IEnumerator PanCamera(Vector3 start, Vector3 end) 
    {
        // Ease camera towards next room
        float t = 0;
        while(t <= 1) {
            Camera.main.transform.position = Mathfx.Hermite(start, end, t);
            t += Time.deltaTime * panSpeed;
            yield return null;
        }

        // Make sure camera ends at right position
        Camera.main.transform.position = end;
        
    }
}
