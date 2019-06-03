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

    PlayerAnimController playerAnim;

    public Transform[] jumpTargets;

    public float jumpSpeed;

    /// <summary>
    /// Called after cat jumps (as soon as he lands)
    /// </summary>
    public UnityEvent afterTable;
    public UnityEvent afterScold;
    List<UnityEvent> jumpEvents = new List<UnityEvent>();

    int i = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        playerAnim = Player.Instance.GetComponent<PlayerAnimController>();

        // hissBox.enabled = true;
        jumpEvents.Add(afterTable);
        jumpEvents.Add(afterScold);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void Jump(Transform t)
    {
        // Trigger jump on anim controller
        anim.SetTrigger("Jump");
        anim.SetBool("Sitting", false);

        // Get jump duration (based on animation length)
        float duration = 2; // Why are we using this starting duration value?
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips.Where(c => c.name.Contains("jump"))) {
            duration += clip.length;
        }

        // Jump towards target
        StartCoroutine(MoveToPosition(t.position, duration, 0));
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
            transform.position = Vector3.Lerp(originalPos, target, t);
            t += speed * Time.deltaTime;
            yield return null;
        }
        // Ensure we don't miss target
        transform.position = target;

        Debug.Log("Cat has landed");

        // Invoke listener
        jumpEvents[i].Invoke();
        if(i<jumpEvents.Count-1) {
            i++;
        }
    }

    public void SetHiss(bool hissing)
    {
        Debug.Log("Cat hissing: " + hissing);

        // Set hiss collider active
        // hissBox.enabled = hissing;

        // Set player scared animation
        playerAnim.SetBool(PlayerAnimController.AnimState.SCARED, hissing);

        // Set anim hissing field
        anim.SetBool("Hissing", hissing);
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

    public void Jump(int target)
    {
        Debug.Log("Jumping to target " + target);
        Jump(jumpTargets[target]);
    }
}
