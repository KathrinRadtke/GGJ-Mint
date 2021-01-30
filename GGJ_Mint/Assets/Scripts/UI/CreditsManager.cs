using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public void QuitButton()
    {
        Application.Quit();
    }

    public void ReloadButton()
    {
        SoundManager.Instance.playMusic("MX_Day2", 0.5f);
        SceneManager.LoadScene(0);
    }
}
