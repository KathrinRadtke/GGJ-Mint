using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    public enum TextType
    {
        Subtitle,
        Phone
    }

    [SerializeField] private Text text;
    [SerializeField] private Text phoneText;
    [SerializeField] private Animator phoneAnimator;
    [SerializeField] private GameObject textBoxHolder;
    [SerializeField] private GameObject phoneHolder;

    public delegate void OnTextFinished();
    public OnTextFinished onTextFinished;

    private int currentTextIndex = 0;
    private string[] currentText;
    private TextType currentTextType;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowNextLine(currentTextType);
        }
    }

    public void PlayText(string[] textToShow, TextType textType = TextType.Subtitle)
    {
        if (textToShow != currentText)
        {
            GameFlowService.Instance.SetMovementAndInteraction(false);
            if (textType == TextType.Subtitle)
            {
                textBoxHolder.SetActive(true);
            }
            else
            {
                phoneHolder.SetActive(true);
            }

            enabled = true;
            currentTextIndex = -1;
            currentText = textToShow;
            currentTextType = textType;
        }
        ShowNextLine(textType);
    }

    public void ShowNextLine(TextType textType)
    {
        currentTextIndex++;
        if (currentTextIndex > currentText.Length - 1)
        {
            GameFlowService.Instance.SetMovementAndInteraction(true);
            textBoxHolder.SetActive(false);
            phoneHolder.SetActive(false);
            enabled = false;
            currentTextIndex = -1;
            currentText = null;
            onTextFinished?.Invoke();
        }
        else
        {
            if (textType == TextType.Subtitle)
            {
                text.text = currentText[currentTextIndex];
            }
            else
            {
                phoneText.text = currentText[currentTextIndex];
            }
        }
    }
}
