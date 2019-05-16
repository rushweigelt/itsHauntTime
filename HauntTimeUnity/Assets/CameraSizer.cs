using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizer : MonoBehaviour
{

    public Camera mainCam;
    public float constantBase;

    private void Awake()
    {
        mainCam = gameObject.GetComponent<Camera>();
    }

    void Start()
    {
        float aspectDifference = 2.1f - mainCam.aspect;

        Debug.Log("Aspect: " + mainCam.aspect + "  -----   " + "Aspect Diff: " + aspectDifference);
        //mainCam.orthographicSize = aspectDifference + constantBase*(aspectDifference + 1f);
        mainCam.orthographicSize = constantBase + (constantBase * (aspectDifference*aspectDifference));
        Debug.Log(mainCam.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
