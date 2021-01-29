using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip AudioClip;
    public AudioMixerGroup mixerGroup;
    [Range(0.3f, 3f)] public float pitch;

    public bool loop;

    [HideInInspector] public AudioSource source;
}
