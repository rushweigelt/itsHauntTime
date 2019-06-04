using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fan : Electronic
{
    //game object that is a collider, used to prevent the player from advancing, simulating push back
    public BoxCollider2D fanRange;

    /// <summary>
    /// Trigger in which player shows Blown animation
    /// </summary>
    public PlayerAnimationTrigger blownAnimationTrigger;

    public ParticleSystem pSystem;

    ParticleSystem.MainModule pSystemMain;

    Animator animator;

    protected override void Start()
    {
        base.Start();

        // Enable windbox collider
        fanRange.enabled = true;
        pSystemMain = pSystem.main;
        animator = GetComponent<Animator>();

        // Play fan blowing sound
        PlaySound(true);
    }

    /// <summary>
    /// Toggle fan collider on/off
    /// </summary>
    /// <param name="active"></param>
    public override void SetActive(bool active)
    {
        // Call base method first
        base.SetActive(active);

        // Set windbox collider
        fanRange.enabled = active;
        blownAnimationTrigger.gameObject.SetActive(active);

        // Stop particle system
        ParticleSystem.EmissionModule emission = pSystem.emission;
        emission.enabled = active;

        // Start/stop fan animation
        animator.SetBool("isOn", active);

        // Set player's blown animation
        // TODO: this is a specific hard-coded case that will only work for our beta: remove this later
        Player.Instance.GetComponent<PlayerAnimController>().SetBool(PlayerAnimController.AnimState.BLOWN_AWAY, active);

        PlaySound(active);
    }

    /// <summary>
    /// Enables/disables fan's looping sound
    /// </summary>
    /// <param name="active">Plays sound when true, stops playing when false</param>
    public void PlaySound(bool active) {
        if(active) {
            SoundController.Instance.PlaySoundEffectLooping(SoundController.SoundType.FAN_BLOWING);
        }
        else {
            SoundController.Instance.StopSoundEffectLooping(SoundController.SoundType.FAN_BLOWING);
        }
    }

    protected override void Interact()
    {
        // TODO: shake the fan a bit to show we rattled it
    }
}
