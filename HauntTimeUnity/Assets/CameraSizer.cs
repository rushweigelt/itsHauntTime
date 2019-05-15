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
        float aspectDifference = 2 - mainCam.aspect;

        Debug.Log(mainCam.aspect + "+" + mainCam.orthographicSize);
        mainCam.orthographicSize = aspectDifference + constantBase*(aspectDifference + .95f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
