﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : Singleton<SoundController>
{
    public enum SoundType
    {
        NONE,
        MAIN_MENU,
        GAME,

        ROOMBA_BEEP,
        CLANG,
        FAN_RATTLE,
        CAT_HISS,
        UNPLUG
    }

    [System.Serializable]
    public class SoundTrack
    {
        public SoundType type;
        public AudioClip clip;

        [Range(0,1)]
        public float volume = 1;
    }

    public List<SoundTrack> backgroundMusic;

    [Space(10)]
    public List<SoundTrack> soundEffects;
    

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Sets background music to track associated with
    /// </summary>
    /// <param name="soundTrack"></param>
    public void SetMusic(SoundType type)
    {
        SoundTrack soundTrack = backgroundMusic.Find(s => s.type == type);
        audioSource.clip = soundTrack.clip;
        audioSource.volume = soundTrack.volume;
    }

    /// <summary>
    /// Plays sound effect associated with SoundType
    /// </summary>
    /// <param name="type">SoundType corresponding to desired sound effect (if multiple sound effects found, will choose one at random)</param>
    public void PlaySoundEffect(SoundType type)
    {
        SoundTrack track = soundEffects.Find(s => s.type == type);
        audioSource.PlayOneShot(track.clip, track.volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
