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
        SceneManager.LoadScene(0);
    }
}
