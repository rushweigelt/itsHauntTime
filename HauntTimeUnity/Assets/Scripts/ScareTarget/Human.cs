using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HumanAnimController))]
public class Human : InteractableObject
{
    HumanAnimController humanAnim;
    public Transform scoldPosition;

    //rate we slow target
    public float slowRate;

    //post-arrival at cat scold event
    public UnityEvent scold;

    //post-arrival event call
    public UnityEvent arrived;


    //Use for Tutorial
    public bool isTutorial;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        humanAnim = GetComponent<HumanAnimController>();
        if(humanAnim == null) {
            Debug.LogWarningFormat("{0} - HumanAnimController component not found; please attach to Human GameObject", name);
        }
    }

    /// <summary>
    /// Move to see the spilled salt
    /// </summary>
    public void Investigate()
    {
        humanAnim.SetAnimationState(HumanAnimController.AnimationState.WALKING);
        Debug.Log("Investigate()");
        StartCoroutine(MoveToPosition(scoldPosition.transform.position, slowRate));
    }

    //Drew's move code, for consistency's sake I reuse it here.
    IEnumerator MoveToInvestigate(Vector3 target, float duration)
    {
        // Save original position
        Vector3 originalPos = transform.position;

        // Calculate speed from duration
        float speed = 1 / duration;

        // Lerp x towards target
        float t = 0;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(originalPos, target, t);
            t += speed * Time.deltaTime;
            yield return null;
        }
        // Ensure we don't miss target
        transform.position = target;

        // Invoke post-move listener
        arrived.Invoke();
    }

    IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        // Save original position
        Vector3 originalPos = transform.position;

        // Calculate speed from duration
        float speed = 1 / duration;

        // Lerp x towards target
        float t = 0;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(originalPos, target, t);
            t += speed * Time.deltaTime;
            yield return null;
        }
        // Ensure we don't miss target
        transform.position = target;
        
        // Invoke post-move listener
        arrived.Invoke();
    }

    public void SetInteract(bool b)
    {
        canInteract = b;
    }

    protected override void Interact()
    {
        if (isTutorial)
        {
            // TODO: handle tutorial situation? what should happen here
        }
        else
        {
            // Player scares human, then game over screen appears
            StartCoroutine(Scare());
        }
    }

    public void Scold()
    {
        Debug.Log("Scold()");

        // Trigger scolding animation
        humanAnim.SetAnimationState(HumanAnimController.AnimationState.SCOLDING);
    }

    public IEnumerator Scare()
    {
        Debug.Log("Scare()");

        // Play ghost spook animation
        PlayerAnimController playerAnim = Player.Instance.GetComponent<PlayerAnimController>();
        playerAnim.SetTrigger(PlayerAnimController.AnimState.SPOOK_HUMAN);
        
        float duration = humanAnim.GetAnimDuration(HumanAnimController.AnimationState.SCARED);

        // Play scared animation
        humanAnim.SetAnimationState(HumanAnimController.AnimationState.SCARED);

        // Wait for animation to complete before showing game over
        yield return new WaitForSeconds(duration);
        OurGameManager.Instance.GameOver(true);
    }
}
