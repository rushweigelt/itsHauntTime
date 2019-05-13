using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cat : InteractableObject
{

    Animator anim;

    public enum AnimationState { SLEEPING, JUMPING, HISSING }

    public AnimationState animationState;

    public Transform jumpTarget;

    public float jumpSpeed;

    /// <summary>
    /// Called after cat jumps (as soon as he lands)
    /// </summary>
    public UnityEvent afterJump;

    //additional box collider for hiss-range
    public BoxCollider2D hissBox;

    public BoxCollider2D detectRange;


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

    public void SetHiss(bool hissing)
    {
        Debug.Log("Cat hissing: " + hissing);

        // Set hiss collider active
        hissBox.enabled = hissing;

        // Set anim hissing field
        anim.SetBool("Hissing", hissing);

        //animationState = AnimationState.HISSING;
    }

    public void Jump()
    {
        // Trigger jump on anim controller
        anim.SetTrigger("Jump");
        anim.SetBool("Sitting", false);

        Debug.Log("Fan was rattled, cat should jump and the hitbox for hissing should be disabled.");
        hissBox.enabled = false;

        // Jump towards target
        float jumpHeight = 3f;
        StartCoroutine(MoveToPosition(jumpTarget.position, jumpSpeed, jumpHeight));
    }

    IEnumerator MoveToPosition(Vector3 target, float duration, float height) {
        // Save original position
        Vector3 originalPos = transform.position;

        // Calculate speed from duration
        float speed = 1 / duration;

        // Lerp x towards target
        float t = 0;
        while(t < 1) {
            transform.position = Vector3.Lerp(originalPos, target, t);
            t += speed * Time.deltaTime;
            yield return null;
        }
        // Ensure we don't miss target
        transform.position = target;

        // Invoke afterJump listeners
        afterJump.Invoke();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hiss");
            SetHiss(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hiss");
            SetHiss(false);
        }
    }
}
