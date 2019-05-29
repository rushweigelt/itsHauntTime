using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Human : InteractableObject
{
    Animator anim;

    public enum AnimationState { SITTING, STANDING_UP, WALKING }

    /// <summary>
    /// Tells Animator which animation to play
    /// </summary>
    public AnimationState animationState;
    public Transform scoldPosition;

    //target we'd be moving to
    // public GameObject target;

    //rate we slow target
    public float slowRate;

    //post-arrival at cat scold event
    public UnityEvent scold;


    //Use for Tutorial
    public bool isTutorial;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public void SetAnimationState(AnimationState state) 
    {
        // Set starting animation state
        anim.SetInteger("State", (int)state);

        // Update current state
        animationState = state;
    }

    /// <summary>
    /// Move to see the spilled salt
    /// </summary>
    public void Investigate()
    {
        Debug.Log("Investigate()");
        StartCoroutine(MoveToPosition(scoldPosition.transform.position, slowRate));
        Scold();
    }

    public void Scold()
    {
        Debug.Log("Scold()");

        // TODO: trigger scolding animation

        // Human can now be scared
        SetInteract(true);
    }

    //Drew's move code, for consistency's sake I reuse it here.
    IEnumerator MoveToInvestigate(Vector3 target, float duration)
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

        // Invoke post-move listener
        // scold.Invoke();
    }

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
    }

    public void SetInteract(bool b)
    {
        canInteract = b;
    }

    protected override void Interact()
    {
        if (isTutorial)
        {
            // TODO: handle tutorial situation? what should happen here
        }
        else
        {
            Scare();
        }
    }

    public void Scare()
    {
        // TODO: play scare animation

        //game over
        OurGameManager.Instance.GameOver(true);
    }
}
