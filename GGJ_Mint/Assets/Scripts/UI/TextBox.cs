using System;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    [SerializeField] private Text text;

    public delegate void OnTextFinished();
    public OnTextFinished onTextFinished;

    private int currentTextIndex = 0;
    private string[] currentText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowNextLine();
        }
    }

    public void PlayText(string[] textToShow)
    {
        if (textToShow != currentText)
        {
            GameFlowService.Instance.SetMovementAndInteraction(false);
            gameObject.SetActive(true);
            enabled = true;
            currentTextIndex = -1;
            currentText = textToShow;
        }
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        currentTextIndex++;
        if (currentTextIndex > currentText.Length - 1)
        {
            GameFlowService.Instance.SetMovementAndInteraction(true);
            gameObject.SetActive(false);
            enabled = false;
            currentTextIndex = -1;
            currentText = null;
            onTextFinished?.Invoke();
        }
        else
        {
            text.text = currentText[currentTextIndex];
        }
    }
}
