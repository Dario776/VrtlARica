using System;
using UnityEngine;

public class AudioManager : SingletonPersistent<AudioManager>
{
    [SerializeField] private Sound[] sounds;
    private bool isMuted = false;

    public override void Awake()
    {
        base.Awake();
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        if (isMuted) return;
        
        Sound sound = Array.Find(sounds, (s) => s.name == name);
        if (sound != null)
            sound.Play();
        else
            Debug.LogWarning("Sound not found in AudioManager: " + name);
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, (s) => s.name == name);
        if (sound != null)
            sound.Stop();
        else
            Debug.LogWarning("Sound not found in AudioManager: " + name);
    }

    public void SetMute(bool mute)
    {
        isMuted = mute;

        foreach (Sound sound in sounds)
        {
            sound.source.mute = mute;
        }
    }

    public bool IsMuted => isMuted;
}
