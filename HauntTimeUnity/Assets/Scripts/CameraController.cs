using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform leftRoomViewpoint;
    public Transform rightRoomViewpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) {
            MoveCamera(leftRoomViewpoint);
        }
        else if(Input.GetKeyDown(KeyCode.D)) {
            MoveCamera(rightRoomViewpoint);
        }
    }

    void MoveCamera(Transform room)
    {
        Camera.main.transform.position = room.position;
    }
}
