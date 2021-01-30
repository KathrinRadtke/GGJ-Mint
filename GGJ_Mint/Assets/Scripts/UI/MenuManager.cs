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
    public SettingsManager m_SettingsManager;
    public GameObject m_SettingsTitle;
    public GameObject m_Master;
    public GameObject m_Music;
    public GameObject m_SFX;
    public GameObject m_Ampience;

    // Hidden Vars
    private bool isMainMenuOpen = false;
    private bool isSettingsOpen = false;
    private bool isAnimationRunning = false;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Time.timeScale = 0;
        AnimateBackground(0);
        AnimateMainMenu(0);
    }

    private void Update()
    {

        // Open and Close Menu
        if (Input.GetKeyDown(KeyCode.Escape) && !isAnimationRunning)
        {
            if (isMainMenuOpen && Time.timeScale == 1)
            {
                AnimateMainMenu(0);
                AnimateBackground(0.5f);
            }

            if (isSettingsOpen)
            {
                AnimateSettings(0);
                AnimateMainMenu(1.1f);
            }
        } else if (Input.GetKeyDown(KeyCode.Tab) && Time.timeScale == 1 && !isAnimationRunning)
        {
            if (!isMainMenuOpen && !isSettingsOpen)
            {
                AnimateBackground(0);
                AnimateMainMenu(0.5f);
            } else if (!isMainMenuOpen && isSettingsOpen)
            {
                AnimateSettings(0);
                AnimateMainMenu(1.1f);
            } else if (isMainMenuOpen && !isSettingsOpen)
            {
                AnimateBackground(0.5f);
                AnimateMainMenu(0);
            }
        }
    }

    #region Menu Animations

    public void AnimateMainMenu(float delay)
    {
        isMainMenuOpen = !isMainMenuOpen;
        isAnimationRunning = true;

        LeanTween.scale(m_MainMenuTitle.gameObject, m_MainMenuTitle.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.3f).setIgnoreTimeScale(true).setDelay(delay).setEaseOutBack().setOnComplete(() =>
        {
            LeanTween.scale(m_PlayButton.gameObject, m_PlayButton.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
            {
                LeanTween.scale(m_SettingsButton.gameObject, m_SettingsButton.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
                {
                    LeanTween.scale(m_QuitButton.gameObject, m_QuitButton.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
                    {
                        isAnimationRunning = false;
                    });
                });
            });
        });
    }

    public void AnimateSettings(float delay)
    {
        isSettingsOpen = !isSettingsOpen;
        m_SettingsManager.ChangeSliderStatus(isSettingsOpen);
        isAnimationRunning = true;

        LeanTween.scale(m_SettingsTitle, m_SettingsTitle.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.3f).setIgnoreTimeScale(true).setDelay(delay).setEaseOutBack().setOnComplete(() =>
        {
            LeanTween.scale(m_Master, m_Master.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
            {
                LeanTween.scale(m_Music, m_Music.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
                {
                    LeanTween.scale(m_SFX, m_SFX.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
                    {
                        LeanTween.scale(m_Ampience, m_Ampience.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
                        {
                            isAnimationRunning = false;
                        });
                    });
                });
            });
        });
    }

    public void AnimateBackground(float delay)
    {
        LeanTween.moveX(m_Background, Mathf.FloorToInt(m_Background.transform.position.x) == -81 ? -501 : -81, 1.3f).setIgnoreTimeScale(true).setDelay(delay).setEaseInOutBack();
    }

    #endregion

    #region Buttons

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        if (!isAnimationRunning)
        {
            Time.timeScale = 1;
            AnimateBackground(0);
            AnimateMainMenu(0);
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);
        GameFlowService.Instance.StartGame();
    }

    public void SettingsButton()
    {
        if (!isAnimationRunning)
        {
            AnimateMainMenu(0);
            AnimateSettings(0.9f);
        }
    }

    #endregion
}
