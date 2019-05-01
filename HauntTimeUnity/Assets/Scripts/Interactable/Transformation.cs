using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Transformation : MonoBehaviour
{
    /// <summary>
    /// Priority determining order of execution relative to other Transformations.
    /// Lower number = higher priority.
    /// </summary>
    public int priority;

    /// <summary>
    /// True if Apply has finished running
    /// </summary>
    public bool finished;

    /// <summary>
    /// Called at end of Apply()
    /// </summary>
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
