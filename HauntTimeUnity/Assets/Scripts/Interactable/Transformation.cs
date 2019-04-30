using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Transformation : MonoBehaviour
{
    public int priority;
    public bool finished;

    public UnityAction onFinished;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Set default OnFinished events
        onFinished += (() => finished = true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Performs simple action on interactable objects.
    /// </summary>
    
    // Override this method in base class, and have it call this base method at end of function
    public virtual void Apply()
    {
    }
}
