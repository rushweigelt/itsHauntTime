using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimController : MonoBehaviour
{
    public enum AnimState {
        IDLE,
        BLOWN_AWAY,
        SPOOK_HUMAN,
        SCARED,
        INTERACT
    }

    Animator animator;

    AnimState prevState;
    AnimState currentState;

    public Dictionary<AnimState, string> stateToParameterMapping = new Dictionary<AnimState, string>() {
        // {AnimState.IDLE, "Idle"},
        {AnimState.BLOWN_AWAY, "Blown Away"},
        {AnimState.SPOOK_HUMAN, "Scare Human"},
        {AnimState.SCARED, "Scared"},
        {AnimState.INTERACT, "Interact"}
    };

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if(animator == null) {
            Debug.LogWarning(name + " - Player doesn't have Animator component; please attach one to game object.");
        }

        // Default to idle state
        currentState = AnimState.IDLE;
    }

    public AnimState SetBool(AnimState state, bool value) 
    {
        // Set parameter value on animator
        string paramName = stateToParameterMapping[state];
        animator.SetBool(paramName, value);

        // Update current/previous states
        prevState = currentState;
        currentState = state;

        return prevState;
    }

    public AnimState SetTrigger(AnimState state) 
    {
        // Set parameter value on animator
        string paramName = stateToParameterMapping[state];
        animator.SetTrigger(paramName);

        // Update current/previous states
        prevState = currentState;
        currentState = state;

        return prevState;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If tagged as fan and has PlayerAnimation component, set animation
        if(other.tag.Equals("AnimationTrigger")) {
            PlayerAnimationTrigger animTrigger = other.GetComponent<PlayerAnimationTrigger>();
            if(animTrigger != null) {
                Debug.LogWarning("Entered AnimationTrigger " + other.name);
                SetBool(animTrigger.playerAnimation, true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // If tagged as fan and has FanBlown component, set animation
        if(other.tag.Equals("AnimationTrigger")) {
            PlayerAnimationTrigger animTrigger = other.GetComponent<PlayerAnimationTrigger>();
            if(animTrigger != null) {
                Debug.LogWarning("Exited AnimationTrigger " + other.name);
                SetBool(animTrigger.playerAnimation, false);
            }
        }
    }
}
