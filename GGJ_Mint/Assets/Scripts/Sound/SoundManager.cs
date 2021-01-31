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
    private List<Sound> m_Musics = new List<Sound>();

    private void Awake()
    {
        InitSoundManager();
        playMusic("MX_Day2", 0.01f);
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

            if (s.source.loop)
            {
                m_Musics.Add(s);
            }
        }

        SetUpMusic();
    }

    public void SetUpMusic()
    {
        foreach (Sound s in m_Musics)
        {
            s.source.volume = 0;
            s.source.Play();
        }
    }

    public void stopAllMusic()
    {

    }

    // Needs to be tested
    public void playMusic(string name, float fadeDuration)
    {
        Sound s = Array.Find(m_Sounds, sound => sound.Name == name);

        if (s == null) return;
        else
        {
            s.source.volume = 1;
            
            if (s.Name.Contains("AMB"))
            {
                foreach (Sound _s in m_Musics)
                {
                    if (!_s.Name.Contains("AMB"))
                        _s.source.volume = 0;
                }
            } else if (s.Name.Contains("MX"))
            {
                foreach (Sound _s in m_Musics)
                {
                    if (!_s.Name.Contains("MX"))
                        _s.source.volume = 0;
                }
            }


            /*
            LeanTween.value(1, 0, fadeDuration).setEaseInOutQuad().setOnUpdate((float val) =>
            {
                currentMusic.source.volume = val;
                if (val >= 0)
                {
                    currentMusic.source.enabled = false;
                    currentMusic.source.Stop();
                }
            });

            s.source.Play();
            LeanTween.value(0, 1, fadeDuration).setEaseInOutQuad().setOnUpdate((float val) =>
            {
                s.source.volume = val;
            });

            */
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
