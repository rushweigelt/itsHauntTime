using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBottle : HeldInteractable
{
    private ParticleSystem pSystem;

    void Start()
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

        // Wait until particle system has finished playing
        float sprayDuration = pSystem.main.duration;
        yield return new WaitForSeconds(sprayDuration);

        Debug.Log("Spray finished");

        // Trigger event on InteractableObject
    }
}
