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
        float aspectDifference = 2.2f - mainCam.aspect;

        Debug.Log(mainCam.aspect + "-----" + aspectDifference);
        mainCam.orthographicSize = aspectDifference + constantBase*(aspectDifference + 1f);
        Debug.Log(mainCam.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
