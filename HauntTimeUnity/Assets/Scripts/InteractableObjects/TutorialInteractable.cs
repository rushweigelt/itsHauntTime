using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInteractable : InteractableObject
{

    protected override void Start()
    {
        
    }

    protected override void Interact()
    {
        // TODO: shake the fan a bit to show we rattled it
    }

    public void RemoveRoomCollider(GameObject roomWall)
    {
        roomWall.SetActive(false);
    }

    public void SwitchSprite(Sprite newSprite)
    {
        SpriteRenderer thisSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        thisSpriteRenderer.sprite = newSprite;
    }

    public void SetAnimationTrigger(string stateName)
    {
        Animator thisAnimator = gameObject.GetComponent<Animator>();
        thisAnimator.SetTrigger(stateName);
        
    }

    public void SetAnimationFloat(string stateName, float value)
    {
        Animator thisAnimator = gameObject.GetComponent<Animator>();
        thisAnimator.SetFloat(stateName, value);
    }

    public void SetAnimationBoolTrue(string stateName)
    {
        Animator thisAnimator = gameObject.GetComponent<Animator>();
        thisAnimator.SetBool(stateName, true);

    }

    public void SetAnimationBoolFalse(string stateName)
    {
        Animator thisAnimator = gameObject.GetComponent<Animator>();
        thisAnimator.SetBool(stateName, false);

    }
}
