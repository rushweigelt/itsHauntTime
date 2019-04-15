using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isDefaultState = true;


    // Start is called before the first frame update
    void Start()
    {
        SetToDefaultState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetToDefaultState()
    {
        isDefaultState = true;
    }
}
