using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cat : InteractableObject
{

    Animator anim;

    public enum AnimationState { SLEEPING, JUMPING, HISSING }

    public AnimationState animationState;

    public Fan fan;

    public BoxCollider2D detectRange;

    /// <summary>
    /// Event called when unplugged
    /// </summary>
    public UnityEvent FanRattle;

    //additional box collider for hiss-range
    public BoxCollider2D hissBox;


    // Start is called before the first frame update
    protected override void Start()
    {
        anim = GetComponent<Animator>();

        //Listener for rattle
        FanRattle.AddListener(() => Jump());

        hissBox.enabled = false;

    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public void Jump()
    {
        hissBox.enabled = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hiss");
            animationState = AnimationState.HISSING;
        }
    }
}
