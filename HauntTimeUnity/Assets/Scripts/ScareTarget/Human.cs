using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Human : InteractableObject
{
    Animator anim;

    //TEMPORARY game ob for endgame text, just until we get a real endgame manager
    public GameObject text;

    public enum AnimationState { SITTING, STANDING_UP, WALKING }

    /// <summary>
    /// Tells Animator which animation to play
    /// </summary>
    public AnimationState animationState;

    //target we'd be moving to
    public GameObject target;

    //rate we slow target
    public float slowRate;

    //post-arrival at cat scold event
    public UnityEvent scold;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        text.SetActive(false);
    }

    public void SetAnimationState(AnimationState state) 
    {
        // Set starting animation state
        anim.SetInteger("State", (int)state);

        // Update current state
        animationState = state;
    }

    public void Investigate()
    {
        StartCoroutine(MoveToInvestigate(target.transform.position, slowRate));
    }

    public void Scold()
    {
        StartCoroutine(MoveToPositionScold(target.transform.position, slowRate));
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
        scold.Invoke();
    }

    IEnumerator MoveToPositionScold(Vector3 target, float duration)
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

        SetInteract(true);
        
    }

    public void SetInteract(bool b)
    {
        canInteract = b;
    }

    protected override void Interact()
    {
        Scare();
    }

    public void Scare()
    {
        //scare animation

        //game over
        text.SetActive(true);
        Time.timeScale = 0;

    }




}
