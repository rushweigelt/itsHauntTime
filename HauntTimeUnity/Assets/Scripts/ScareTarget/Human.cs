using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Human : InteractableObject
{
    Animator anim;

    public enum AnimationState { 
        SITTING = 0, 
        STANDINGUP, 
        WALKING, 
        SCOLDING, 
        SCARED 
    }

    /// <summary>
    /// Tells Animator which animation to play
    /// </summary>
    public AnimationState animationState;
    public Transform scoldPosition;

    //rate we slow target
    public float slowRate;

    //post-arrival at cat scold event
    public UnityEvent scold;

    //post-arrival event call
    public UnityEvent arrived;


    //Use for Tutorial
    public bool isTutorial;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();

        float duration = GetAnimDuration(AnimationState.SCARED);
    }

    public void SetAnimationState(AnimationState state) 
    {
        // Set starting animation state
        anim.SetInteger("State", (int)state);
        Debug.LogWarning("Setting animation state to " + state);

        // Update current state
        animationState = state;
    }

    public float GetAnimDuration(AnimationState state) {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        // Get clip
        foreach(AnimationClip clip in clips) {
            if(clip.name.ToLower().Contains(state.ToString().ToLower())) {
                Debug.LogFormat("Found clip {0} with duration: {1} seconds", clip.name, clip.length);
                return clip.length;
            }
        }
        
        // Return duration
        Debug.LogWarningFormat("GetAnimDuration() - failed to find duration for {0} animation (search by filename failed)", state);
        return 0;
    }

    /// <summary>
    /// Move to see the spilled salt
    /// </summary>
    public void Investigate()
    {
        SetAnimationState(AnimationState.WALKING);
        Debug.Log("Investigate()");
        StartCoroutine(MoveToPosition(scoldPosition.transform.position, slowRate));
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
        arrived.Invoke();
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
        
        // Invoke post-move listener
        arrived.Invoke();
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
            StartCoroutine(Scare());
        }
    }

    public void Scold()
    {
        Debug.Log("Scold()");

        // TODO: trigger scolding animation
        SetAnimationState(AnimationState.SCOLDING);

        // Human can now be scared
        SetInteract(true);
    }

    public IEnumerator Scare()
    {
        Debug.Log("Scare()");
        float duration = GetAnimDuration(AnimationState.SCARED);

        // Play scared animation
        SetAnimationState(AnimationState.SCARED);

        // Wait for animation to complete before showing game over
        // yield return new WaitForSeconds(duration);
        yield return new WaitForSeconds(0); // currently not working, disabled the wait
        OurGameManager.Instance.GameOver(true);
    }
}
