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

    public Transform[] jumpTargets;

    public float jumpSpeed;

    /// <summary>
    /// Called after cat jumps (as soon as he lands)
    /// </summary>
    public UnityEvent afterTable;
    public UnityEvent afterScold;
    public List<UnityEvent> jumpEvents;
    int i = 0;

    //additional box collider for hiss-range
    public BoxCollider2D hissBox;

    public BoxCollider2D detectRange;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();

        hissBox.enabled = true;
        jumpEvents.Add(afterTable);
        jumpEvents.Add(afterScold);
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

    public void Jump(Transform t)
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
        StartCoroutine(MoveToPosition(t.position, duration, jumpHeight));
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
            //Debug.Log("T: " + t);
            transform.position = Vector3.Lerp(originalPos, target, t);
            t += speed * Time.deltaTime;
            yield return null;
        }
        // Ensure we don't miss target
        transform.position = target;

        Debug.Log(jumpEvents.ToList());

        Debug.Log("Cat has landed");

        //listener
        jumpEvents[i].Invoke();
        if(i<jumpEvents.Count-1)
        {
            i++;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hiss");
            SetHiss(true);

            // Play hiss sound effect
            SoundController.Instance.PlaySoundEffect(SoundController.SoundType.CAT_HISS);
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

    public void ToTable()
    {
        Jump(jumpTargets[0]);
        //afterTable.Invoke();
    }

    public void BeScolded()
    {
        Jump(jumpTargets[1]);
        //afterScold.Invoke();
    }
}
