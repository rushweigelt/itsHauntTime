using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : ScareTarget
{
    Animator anim;

    public enum AnimationState { SITTING, STANDING_UP, SPOOKED, CLEANING, WALKING }

    /// <summary>
    /// Tells Animator which animation to play
    /// </summary>
    public AnimationState animationState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // TODO: set starting animation state
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    //AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        //    if (inRange == true)
        //    {
        //        anim.SetTrigger(gettingUpHash);
        //    }
        //}
    }
}
