using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaltShaker : SpriteSwapInteraction
{
    /// <summary>
    /// Collider preventing player from passing
    /// </summary>
    public GameObject saltWall;

    //an event to signal an audio cue worth investigating has occured
    public UnityEvent audioInvestigate;

    //audio of salt spilling
    AudioSource aSource;
    public AudioClip clang;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        saltWall.SetActive(false);
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void Spill()
    {
        base.Interact();
        saltWall.SetActive(true);
        aSource.PlayOneShot(clang);
        audioInvestigate.Invoke();

    }

    public void BlowAway()
    {
        saltWall.SetActive(false);
    }
}
