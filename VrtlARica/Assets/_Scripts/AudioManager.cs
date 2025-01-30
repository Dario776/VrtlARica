using System;
using UnityEngine;

public class AudioManager : SingletonPersistent<AudioManager>
{
    [SerializeField] private Sound[] sounds;
    private bool isHomePageMuted;
    private bool isMuted;

    public override void Awake()
    {
        base.Awake();
        InitializeSounds();
        LoadMuteStates();
    }
    private void InitializeSounds()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void LoadMuteStates()
    {
        isHomePageMuted = PlayerPrefs.GetInt("IsHomePageMuted", 0) == 1;
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;
        ApplyMuteStates();
    }

    public void Play(string name)
    {
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

    public void SetHomePageMute(bool mute)
    {
        isHomePageMuted = mute;
        ApplyMuteState("mainmenumusic", mute);
    }

    public void SetMute(bool mute)
    {
        isMuted = mute;
        foreach (Sound sound in sounds)
        {
            if (sound.name != "mainmenumusic")
            {
                sound.source.mute = mute;
            }
        }
    }

    private void ApplyMuteStates()
    {
        SetHomePageMute(isHomePageMuted);
        SetMute(isMuted);
    }

    private void ApplyMuteState(string soundName, bool mute)
    {
        Sound sound = Array.Find(sounds, s => s.name == soundName);
        if (sound != null && sound.source != null) sound.source.mute = mute;
    }
    public bool IsHomePageMuted => isHomePageMuted;
    public bool IsMuted => isMuted;
}
