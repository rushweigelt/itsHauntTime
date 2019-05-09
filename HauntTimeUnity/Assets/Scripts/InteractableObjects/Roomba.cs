using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomba : RemoteElectronic
{
    public ParticleSystem pSystem;

    ParticleSystem.MainModule pSystemMain;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        pSystemMain = pSystem.main;
    }

    public override void SetActive(bool active)
    {
        // Call base method first
        base.SetActive(active);

        Debug.Log("Roomba.SetActive(" + active + ")");

        // Stop particle system
        ParticleSystem.EmissionModule emission = pSystem.emission;
        emission.enabled = active;
    }
}
