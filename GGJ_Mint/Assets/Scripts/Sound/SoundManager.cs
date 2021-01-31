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
        playMusic("MX_Day3", 0.01f);
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

    // Needs to be tested
    public void playMusic(string name, float fadeDuration)
    {
        Sound s = Array.Find(m_Sounds, sound => sound.Name == name);
        Sound currentMusic = Array.Find(m_Sounds, sound => sound.source.isPlaying);

        if (s == null) return;

        if (currentMusic != null && s != null)
        {
            LeanTween.value(1, 0, fadeDuration).setEaseInOutQuad().setOnUpdate((float val) =>
            {
                currentMusic.source.volume = val;
                if (val == 0)
                {
                    Debug.Log("stop current music");
                    currentMusic.source.Stop();
                }
            });

            s.source.Play();
            LeanTween.value(0, 1, fadeDuration).setEaseInOutQuad().setOnUpdate((float val) =>
            {
                s.source.volume = val;
            });
        }

        if (currentMusic == null && s != null) s.source.Play();
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
