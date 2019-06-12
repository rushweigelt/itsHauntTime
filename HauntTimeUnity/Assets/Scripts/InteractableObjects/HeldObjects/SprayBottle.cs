using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBottle : HeldInteractable
{
    private ParticleSystem pSystem;

    [Header("Spray Bottle Settings")]
    
    /// <summary>
    /// Number of busts to spray
    /// </summary>
    public int numberOfSprays = 1;

    /// <summary>
    /// Interval between each spray (in seconds)
    /// </summary>
    public float sprayInterval = 1;

    override
    protected void Start()
    {
        pSystem = GetComponentInChildren<ParticleSystem>();
    }

    override
    public void UseOn(InteractableObject obj) {
        StartCoroutine(UseWithDelay(obj));
    }

    IEnumerator UseWithDelay(InteractableObject obj) {
        // Play particle system
        pSystem.Play();
        Debug.Log("Spray started");

        StartCoroutine(PlaySoundEffect(sprayInterval, numberOfSprays));

        // Wait until particle system has finished playing
        float sprayDuration = pSystem.main.duration;
        yield return new WaitForSeconds(sprayDuration);

        Debug.Log("Spray finished");

        // Trigger event on InteractableObject
        // TODO: make this apply to whatever Interactable you use it on, this is just a hardcoded solution for the cat
        Cat cat = (Cat)obj;

        // Jump to first jump target
        cat.Jump(0);
    }

    IEnumerator PlaySoundEffect(float interval, int cycles) {
        for(int i = 0; i < cycles; i++) {
            // Play sound effect
            SoundController.Instance.PlaySoundEffect(usedSound);

            // Wait interval before playing again
            yield return new WaitForSeconds(interval);
        }
    }
}
