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

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        saltWall.SetActive(false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void Spill()
    {
        base.Interact();

        // Activate spilled salt trail (blocks hatto from progressing)
        saltWall.SetActive(true);

        // Play sound effect of salt shaker hitting floor
        SoundController.Instance.PlaySoundEffect(SoundController.SoundType.CLANG);

        // Call event associated with salt falling over
        audioInvestigate.Invoke();
    }

    public void BlowAway()
    {
        saltWall.SetActive(false);
    }
}
