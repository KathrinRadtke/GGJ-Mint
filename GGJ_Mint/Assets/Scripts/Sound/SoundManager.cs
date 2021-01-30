using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Sound Manager Settings")]
    public AudioMixer m_Mixer;

    public Sound[] m_Sounds;

    private void Start()
    {
        InitSoundManager();
    }

    public void InitSoundManager()
    {
        foreach (Sound s in m_Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.AudioClip;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }

    public void playSound(string name)
    {
       Sound s = Array.Find(m_Sounds, sound => sound.Name == name);

       if (s != null) s.source.Play();
    }

    public void stopSound(string name)
    {
        Sound s = Array.Find(m_Sounds, sound => sound.Name == name);

        if (s != null) s.source.Stop();
    }
}
