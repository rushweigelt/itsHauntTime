using System.Collections;
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
        UNPLUG,
        ROOMBA_DEAD,

        // LOOPING
        FAN_BLOWING
    }

    [System.Serializable]
    public class SoundTrack
    {
        public SoundType type;
        public AudioClip clip;

        [Range(0,1)]
        public float volume = 1;
        public bool loop = false;
    }

    /// <summary>
    /// Background music played in scene
    /// </summary>
    public SoundType music;

    [Header("Sound Database")]

    // TODO: separate Music from SoundEffects

    /// <summary>
    /// Collection of all background music
    /// </summary>
    public List<SoundTrack> backgroundMusic;

    [Space(10)]
    /// <summary>
    /// Collection of all sound effects
    /// </summary>
    public List<SoundTrack> soundEffects;

    /// <summary>
    /// Mapping of SoundTracks to their respective audio sources.
    /// NOTE: SoundTracks should only use their own audio source if they must be looped.
    /// </summary>
    public Dictionary<SoundTrack, AudioSource> soundEffectToAudioSourceMap = new Dictionary<SoundTrack, AudioSource>();
    

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Set music to selected track
        SetMusic(music);
    }

    /// <summary>
    /// Sets background music to track associated with
    /// </summary>
    /// <param name="soundTrack"></param>
    public void SetMusic(SoundType type)
    {
        if(type.Equals(SoundType.NONE)) {
            return;
        }
        SoundTrack soundTrack = backgroundMusic.Find(s => s.type == type);
        audioSource.clip = soundTrack.clip;
        audioSource.volume = soundTrack.volume;

        audioSource.Play();
    }

    /// <summary>
    /// Plays sound effect associated with SoundType
    /// </summary>
    /// <param name="type">SoundType corresponding to desired sound effect (if multiple sound effects found, will choose one at random)</param>
    public void PlaySoundEffect(SoundType type)
    {
        if(type.Equals(SoundType.NONE)) {
            return;
        }
        SoundTrack track = soundEffects.Find(s => s.type == type);
        audioSource.PlayOneShot(track.clip, track.volume);
    }

    public void PlaySoundEffectLooping(SoundType type) 
    {
        Debug.Log("Playing looping sound effect: " + type);
        AudioSource audioSource;
        SoundTrack track = soundEffects.Find(i => i.type == type);

        if(!soundEffectToAudioSourceMap.TryGetValue(track, out audioSource)) {
            Debug.LogFormat("AudioSource not found for {0} effect; creating one now", type);

            // Instantiate AudioSource as child
            audioSource = Instantiate(new GameObject().AddComponent<AudioSource>(), transform);
            audioSource.name = type.ToString() + "_AudioSource";
            
            // Configure with track's properties
            audioSource.clip = track.clip;
            audioSource.loop = track.loop;
            audioSource.volume = track.volume;

            // Add to mapping
            soundEffectToAudioSourceMap.Add(track, audioSource);
        }

        // Play sound effect
        audioSource.Play();
    }

    public void StopSoundEffectLooping(SoundType type) {
        Debug.Log("Playing looping sound effect: " + type);
        AudioSource audioSource;
        SoundTrack track = soundEffects.Find(i => i.type.Equals(type));

        if(!soundEffectToAudioSourceMap.TryGetValue(track, out audioSource)) {
            Debug.LogWarningFormat("AudioSource not found for {0} effect", type);
            return;
        }
        audioSource.Stop();
    }
}
