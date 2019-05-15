using System.Collections;
using System.Linq;
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

        hissBox.enabled = false;

        // Height of jump
        // TODO: actually use this height
        float jumpHeight = 3f;

        // Get jump duration
        float duration = 0;
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips.Where(c => c.name.Contains("jump"))) {
            duration += clip.length;
        }

        // Jump towards target
        StartCoroutine(MoveToPosition(jumpTarget.position, duration, jumpHeight));
    }

    IEnumerator MoveToPosition(Vector3 target, float duration, float height) {
        Debug.Log("Cat is jumping");

        // Save original position
        Vector3 originalPos = transform.position;

        // Calculate speed from duration
        float speed = 1 / duration;

        // Lerp x towards target
        float t = 0;
        while(t < 1) {
            Debug.Log("T: " + t);
            transform.position = Vector3.Lerp(originalPos, target, t);
            t += speed * Time.deltaTime;
            yield return null;
        }
        // Ensure we don't miss target
        transform.position = target;

        Debug.Log("Cat has landed");

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
