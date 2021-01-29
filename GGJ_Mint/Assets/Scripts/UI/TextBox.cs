using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    [SerializeField] private Text text;

    public delegate void OnTextFinished();
    public OnTextFinished onTextFinished;

    private int currentTextIndex = 0;
    private string[] currentText;

    public void PlayText(string[] textToShow)
    {
        gameObject.SetActive(true);
        currentTextIndex = -1;
        currentText = textToShow;
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        currentTextIndex++;
        if (currentTextIndex > currentText.Length - 1)
        {
            gameObject.SetActive(false);
            onTextFinished?.Invoke();
        }
        else
        {
            text.text = currentText[currentTextIndex];
        }
    }
}
