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
