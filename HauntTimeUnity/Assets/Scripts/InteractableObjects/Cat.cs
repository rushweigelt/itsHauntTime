using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cat : InteractableObject
{

    Animator anim;

    public enum AnimationState { SLEEPING, JUMPING, HISSING }

    public AnimationState animationState;

    public SaltShaker salt;

    public GameObject saltOb;

    public BoxCollider2D detectRange;

    /// <summary>
    /// Called after cat jumps (as soon as he lands)
    /// </summary>
    public UnityEvent afterJump;

    public float moveRate;

    //additional box collider for hiss-range
    public BoxCollider2D hissBox;


    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();
        anim = GetComponent<Animator>();

        hissBox.enabled = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void Hiss()
    {
        Debug.Log("Hiss");
        hissBox.enabled = true;
    }

    public void Jump()
    {
        // Trigger jump on anim controller
        anim.SetTrigger("Jump");

        Vector3 velo = new Vector3(moveRate, 0, 0);
        Debug.Log("Fan was rattled, cat should jump and the hitbox for hissing should be disabled.");
        hissBox.enabled = false;

        while(saltOb.transform.position.x >= transform.position.x)
        {
            transform.position += velo * Time.deltaTime;
        }

        // Knock over salt
        //salt.CatJump.Invoke();

        // Invoke afterJump listeners
        afterJump.Invoke();
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
