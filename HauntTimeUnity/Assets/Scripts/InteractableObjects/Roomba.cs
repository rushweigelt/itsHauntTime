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
    public UnityEvent Vacuum;

    //Animator
    public Animator roombaAnimator;

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
        else
        {
            TurnOn();
        }
    }

    public void TurnOn()
    {
        Debug.Log("Roomba was turned on");
        StartCoroutine(MoveToPosition(dest.transform.position, slowRate));
        isOn = true;
        roombaAnimator.SetBool("TurnedOn", true);
    }

    public void TurnOff()
    {
        isOn = false;
        roombaAnimator.SetBool("TurnedOn", false);
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
        Vacuum.Invoke();

        // Play sound when reached target
        //aSource.PlayOneShot(fullBeep);
        SoundController.Instance.PlaySoundEffect(SoundController.SoundType.ROOMBA_BEEP);

        TurnOff();
    }
}
