using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Electronic
{
    //game object that is a collider, used to prevent the player from advancing, simulating push back
    public BoxCollider2D fanRange;

    public ParticleSystem pSystem;

    ParticleSystem.MainModule pSystemMain;

    protected override void Start()
    {
        base.Start();

        fanRange.enabled = true;
        pSystemMain = pSystem.main;
    }
    
    /// <summary>
    /// Toggle fan collider on/off
    /// </summary>
    /// <param name="active"></param>
    public override void SetActive(bool active)
    {
        // Call base method first
        base.SetActive(active);

        // Debug.Log("Fan.SetActive(" + active + ")");

        // Set fan collider off
        fanRange.enabled = active;

        // Stop particle system
        ParticleSystem.EmissionModule emission = pSystem.emission;
        emission.enabled = active;
    }
}
