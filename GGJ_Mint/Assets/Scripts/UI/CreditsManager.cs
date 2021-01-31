using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public MenuManager m_MenuManager;
    public void QuitButton()
    {
        Application.Quit();
    }

    public void ReloadButton()
    {
        m_MenuManager.m_PlayButton.SetActive(true);
        SoundManager.Instance.playMusic("MX_Day2", 0.5f);
        SceneManager.LoadScene(0);
    }
}
