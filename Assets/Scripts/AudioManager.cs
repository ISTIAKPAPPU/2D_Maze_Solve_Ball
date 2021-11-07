using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds;

    // [HideInInspector] public bool canSoundPlayable;

    public enum SoundType
    {
        Menu,
        Game,
        Win,
        Hit
    }

    private void Start()
    {
        Play(SoundType.Game);
    }

    private void Awake()
    {
        //  canSoundPlayable = true;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // private bool CanPlaySound(SoundType sound)
    // {
    //     switch (sound)
    //     {
    //         case SoundType.Hit:
    //             return canSoundPlayable;
    //         case SoundType.Game:
    //             break;
    //         default:
    //             return true;
    //     }
    // }

    public void Play(SoundType soundName)
    {
        var s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }

        // if (CanPlaySound(soundName))
        // {
        s.source.Play();
        // }
    }

    public void Stop(SoundType soundName)
    {
        var s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }

        s.source.Stop();
    }

    public void MuteSound()
    {
        foreach (var s in sounds)
        {
            s.source.mute = true;
        }
    }

    public void UnMuteSound()
    {
        foreach (var s in sounds)
        {
            if (!(s.name == SoundType.Game || s.name == SoundType.Menu))
                s.source.mute = false;
            else
            {
                s.source.mute = PlayerPrefs.GetInt("musicMuted") == 1;
            }
        }
    }

    public void MuteMusic()
    {
        foreach (var s in sounds)
        {
            if (s.name == SoundType.Game || s.name == SoundType.Menu)
                s.source.mute = true;
        }
    }

    public void UnMuteMusic()
    {
        foreach (var s in sounds)
        {
            if (s.name == SoundType.Game || s.name == SoundType.Menu)
                s.source.mute = PlayerPrefs.GetInt("soundMuted") == 1;
        }
    }
    
}