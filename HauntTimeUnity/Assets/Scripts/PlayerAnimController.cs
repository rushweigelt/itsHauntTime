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
    }

    public void SetBool(AnimState state, bool value) 
    {
        string paramName = stateToParameterMapping[state];
        animator.SetBool(paramName, value);
    }

    public void SetTrigger(AnimState state) {
        string paramName = stateToParameterMapping[state];
        animator.SetTrigger(paramName);
    }
}
