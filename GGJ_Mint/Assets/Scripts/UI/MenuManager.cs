using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    public GameObject m_Background;

    [Header("Areas")]
    public GameObject m_MainMenuArea;
    public GameObject m_SettingsArea;

    [Header("Main Menu Objects")]
    [SerializeField]
    private Animator backgroundAnimator;
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

        AnimateBackground(0, true);
        AnimateMainMenu(0, true);

    }

    private void Update()
    {
        InputManagement();
    }

    public void InputManagement()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab)) && !isAnimationRunning)
        {
            if (Time.timeScale == 1)
            {
                if (isMainMenuOpen && !isSettingsOpen)
                {
                    AnimateBackground(0.5f, false);
                    AnimateMainMenu(0, false);
                } else if (isSettingsOpen && !isMainMenuOpen)
                {
                    AnimateSettings(0, false);
                    AnimateMainMenu(0.5f, true);
                } else if (!isMainMenuOpen && !isSettingsOpen)
                {
                    AnimateBackground(0, true);
                    AnimateMainMenu(0.5f, true);
                }
            } else if (Time.timeScale == 0)
            {
                if (isSettingsOpen && !isMainMenuOpen)
                {
                    AnimateSettings(0, false);
                    AnimateMainMenu(0.5f, true);
                }
            }
        }
    }

    #region Menu Animations

    public void AnimateMainMenu(float delay, bool active)
    {
        Vector3 scale = active ? Vector3.one : Vector3.zero;

        isAnimationRunning = true;

        LeanTween.scale(m_MainMenuTitle.gameObject, scale, 0.3f).setIgnoreTimeScale(true).setDelay(delay).setEaseOutBack().setOnComplete(() =>
        {
            LeanTween.scale(m_PlayButton.gameObject, scale, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
            {
                LeanTween.scale(m_SettingsButton.gameObject, scale, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
                {
                    LeanTween.scale(m_QuitButton.gameObject, scale, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
                    {
                        isMainMenuOpen = active;
                        isAnimationRunning = false;
                    });
                });
            });
        });
    }

    public void AnimateSettings(float delay, bool active)
    {
        Vector3 scale = active ? Vector3.one : Vector3.zero;
        m_SettingsManager.ChangeSliderStatus(active);
        isAnimationRunning = true;

        LeanTween.scale(m_SettingsTitle, scale, 0.3f).setIgnoreTimeScale(true).setDelay(delay).setEaseOutBack().setOnComplete(() =>
        {
            LeanTween.scale(m_Master, scale, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
            {
                LeanTween.scale(m_Music, scale, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
                {
                    LeanTween.scale(m_SFX, scale, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
                    {
                        LeanTween.scale(m_Ampience, scale, 0.2f).setIgnoreTimeScale(true).setEaseOutBack().setOnComplete(() =>
                        {
                            isSettingsOpen = active;
                            isAnimationRunning = false;
                        });
                    });
                });
            });
        });
    }

    public void AnimateBackground(float delay, bool active)
    {
        
        float xPos = active ? -250 : -2000;
        LeanTween.moveLocalX(m_Background, xPos, 1.3f).setIgnoreTimeScale(true).setDelay(delay).setEaseInOutBack();

        
        Debug.Log("animarte background " + xPos);

        /*
        if (active)
        {
            backgroundAnimator.SetTrigger("show");
        }
        else
        {
            backgroundAnimator.SetTrigger("hide");
        }
        */

    }

    #endregion

    #region Buttons

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        if (!isAnimationRunning && Time.timeScale == 1)
        {
            SceneManager.LoadScene(0);
        }
        else if (!isAnimationRunning && Time.timeScale == 0)
        {
            Time.timeScale = 1;
            AnimateMainMenu(0, false);
            AnimateBackground(0, false);
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
            AnimateMainMenu(0, false);
            AnimateSettings(0.9f, true);
        }
    }

    #endregion
}
