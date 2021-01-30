using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    public GameObject m_Background;

    [Header("Areas")]
    public GameObject m_MainMenuArea;
    public GameObject m_SettingsArea;

    [Header("Main Menu Objects")]
    public GameObject m_MainMenuTitle;
    public GameObject m_PlayButton;
    public GameObject m_SettingsButton;
    public GameObject m_QuitButton;

    [Header("Settings Objects")]
    public GameObject m_SettingsTitle;
    public GameObject m_Master;
    public GameObject m_Music;
    public GameObject m_SFX;
    public GameObject m_Ampience;

    // Hidden Vars
    private bool isMainMenuOpen = false;
    private bool isSettingsOpen = false;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        AnimateBackground(0);
        AnimateMainMenu(0);
    }

    private void Update()
    {

        // Open and Close Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMainMenuOpen)
            {
                AnimateMainMenu(0);
                AnimateBackground(0.5f);
            }

            if (isSettingsOpen)
            {
                AnimateSettings(0);
                AnimateMainMenu(1.1f);
            }
        }
    }

    public void AnimateMainMenu(float delay)
    {
        LeanTween.scale(m_MainMenuTitle.gameObject, m_MainMenuTitle.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.3f).setDelay(delay).setEaseOutBack().setOnComplete(() =>
        {
            LeanTween.scale(m_PlayButton.gameObject, m_PlayButton.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setEaseOutBack().setOnComplete(() =>
            {
                LeanTween.scale(m_SettingsButton.gameObject, m_SettingsButton.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setEaseOutBack().setOnComplete(() =>
                {
                    LeanTween.scale(m_QuitButton.gameObject, m_QuitButton.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setEaseOutBack().setOnComplete(() =>
                    {
                        isMainMenuOpen = !isMainMenuOpen;
                    });
                });
            });
        });
    }

    public void AnimateSettings(float delay)
    {
        LeanTween.scale(m_SettingsTitle, m_SettingsTitle.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.3f).setDelay(delay).setEaseOutBack().setOnComplete(() =>
        {
            LeanTween.scale(m_Master, m_Master.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setEaseOutBack().setOnComplete(() =>
            {
                LeanTween.scale(m_Music, m_Music.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setEaseOutBack().setOnComplete(() =>
                {
                    LeanTween.scale(m_SFX, m_SFX.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setEaseOutBack().setOnComplete(() =>
                    {
                        LeanTween.scale(m_Ampience, m_Ampience.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setEaseOutBack().setOnComplete(() =>
                        {
                            isSettingsOpen = !isSettingsOpen;
                        });
                    });
                });
            });
        });
    }

    public void AnimateBackground(float delay)
    {
        LeanTween.moveX(m_Background, m_Background.transform.position.x == -80 ? -500 : -80, 1.3f).setDelay(delay).setEaseInOutBack();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        AnimateBackground(0.5f);
        AnimateMainMenu(0);
    }

    public void SettingsButton()
    {
        AnimateMainMenu(0);
        AnimateSettings(0.9f);
    }
}
