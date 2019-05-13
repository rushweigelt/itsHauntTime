using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomba : RemoteElectronic
{
    public ParticleSystem pSystem;

    public GameObject dest;

    ParticleSystem.MainModule pSystemMain;

    public float moveRate;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        pSystemMain = pSystem.main;
        isOn = false;
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
        Vector3 velo = new Vector3 (moveRate, 0, 0);
        while(dest.transform.position.x != transform.position.x)
        {
            transform.position += velo * Time.deltaTime;
        }
        isOn = true;
    }

    public void TurnOff()
    {
        isOn = false;
    }
}
