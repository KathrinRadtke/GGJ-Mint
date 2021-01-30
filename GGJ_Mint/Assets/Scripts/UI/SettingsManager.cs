using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [Header("Settings Slider")]
    public AudioMixer m_Mixer;
    public Slider m_MasterVolume;
    public Slider m_MusicVolume;
    public Slider m_SFXVolume;
    public Slider m_AmbienceVolume;

    public void ChangeSliderStatus(bool active)
    {
        m_MasterVolume.enabled = active;
        m_AmbienceVolume.enabled = active;
        m_MusicVolume.enabled = active;
        m_SFXVolume.enabled = active;
    }

    #region Music Volume Slider
    public void setSFXVol(float vol) => m_Mixer.SetFloat("SFX_Volume", vol);
    public void setMasterVol(float vol) => m_Mixer.SetFloat("Master_Volume", vol);
    public void setAmbienceVol(float vol) => m_Mixer.SetFloat("Ambience_Volume", vol);
    public void setMusicVol(float vol) => m_Mixer.SetFloat("Music_Volume", vol);
    #endregion
}
