using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : InteractableObject
{
    Animator anim;

    public enum AnimationState { SITTING, STANDING_UP, WALKING }

    /// <summary>
    /// Tells Animator which animation to play
    /// </summary>
    public AnimationState animationState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetAnimationState(AnimationState state) 
    {
        // Set starting animation state
        anim.SetInteger("State", (int)state);

        // Update current state
        animationState = state;
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimationState(animationState);
    }

    
}
