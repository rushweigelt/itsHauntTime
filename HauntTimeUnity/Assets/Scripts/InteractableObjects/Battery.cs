using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Battery : InteractableObject
{
    public Transform holdTrans; //the transform where we hold items at--on the Player game obj

    [Range(0,1)]
    public float heldScale;

    public bool held; //a boolean that lets us know we're holding the battery
    public Transform dest; //the destination of where the battery could go
    public float slowRate; //the rate we slow down the insert function
    public UnityEvent inserted; //an event that will trigger "Turn On" in boomba
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        held = false;
    }

    protected override void Update()
    {
        base.Update();
        //keep battery object pinned to the held-item location.
        if (held)
        {
            this.transform.position = holdTrans.position;
        }


    }

    protected override void Interact()
    {
        PickUp();
    }
    //a function where we insert the battery into applicable items
    public void InsertBattery()
    {
        StartCoroutine(MoveToPosition(dest.position, slowRate));
    }
    //function for picking up--our Interact function
    public void PickUp()
    {
        //pin, change boolean, then shrink item's scale
        this.transform.position = holdTrans.position;
        held = true;
        transform.localScale = Vector3.one * heldScale;
    }

    //Drew's move code, for consistency's sake I reuse it here.
    IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        // Save original position
        Vector3 originalPos = transform.position;

        // Calculate speed from duration
        float speed = 1 / duration;

        // Lerp x towards target
        float t = 0;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(originalPos, target, t);
            t += speed * Time.deltaTime;
            yield return null;
        }
        // Ensure we don't miss target
        transform.position = target;

        //disable gameob
        held = false;
        gameObject.SetActive(false);

        // Invoke post-move listener
        inserted.Invoke();

    }
}
