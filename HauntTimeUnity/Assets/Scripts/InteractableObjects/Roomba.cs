using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Roomba : RemoteElectronic
{
    //public ParticleSystem pSystem;

    public GameObject dest;

    //ParticleSystem.MainModule pSystemMain;

    public float slowRate;

    //Event listener for after thbe vacuum reaches it's destination.
    public UnityEvent ReachedDestination;

    //Animator
    public Animator roombaAnimator;

    //Battery, since every roomba would have one.
    public Battery battery;

    public GameObject questionMarkPrompt;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //pSystemMain = pSystem.main
        isOn = false;
    }

    public override void SetActive(bool active)
    {
        // Call base method first
        base.SetActive(active);

        Debug.Log("Roomba.SetActive(" + active + ")");

        // Stop particle system
        //ParticleSystem.EmissionModule emission = pSystem.emission;
        //emission.enabled = active;
    }

    protected override void Interact()
    {
        if(isOn)
        {
            TurnOff();
        }
        else if(isOn == false && battery.held)
        {
            //insert battery, which invokes TurnOn.
            battery.InsertBattery();
        }
        else
        {
            OutOfBattery();
        }
    }

    public void TurnOn()
    {

        //set question mark to prompt
        SetPrompt();
        Debug.Log("Roomba was turned on");
        // Play vaccuum sound effect
        SoundController.Instance.PlaySoundEffectLooping(SoundController.SoundType.ROOMBA_MOVE);

        // Move towards salt
        StartCoroutine(MoveToPosition(dest.transform.position, slowRate));

        // TODO: this is happening again in SetActive() - condense this to one location
        isOn = true;
        roombaAnimator.SetBool("TurnedOn", true);
    }

    public void TurnOff()
    {
        // TODO: this is happening again in SetActive() - condense this to one location
        isOn = false;
        roombaAnimator.SetBool("TurnedOn", false);
    }

    public void OutOfBattery()
    {
        //add function to display out of battery icon
        isOn = false;
        SoundController.Instance.PlaySoundEffect(SoundController.SoundType.ROOMBA_DEAD);
    }

    //Drew's move code, for consistency's sake I reuse it here.
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
        ReachedDestination.Invoke();

        // Stop the vaccuum sound effect
        SoundController.Instance.StopSoundEffectLooping(SoundController.SoundType.ROOMBA_MOVE);

        // Play sound when reached target
        SoundController.Instance.PlaySoundEffect(SoundController.SoundType.ROOMBA_BEEP);

        TurnOff();
    }

    public void SetPrompt()
    {
        base.interactRange.enabled = false;
    }
}
