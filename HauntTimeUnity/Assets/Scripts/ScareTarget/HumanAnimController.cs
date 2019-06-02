using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimController : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        // Set starting animation state
        SetAnimationState(animationState);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
