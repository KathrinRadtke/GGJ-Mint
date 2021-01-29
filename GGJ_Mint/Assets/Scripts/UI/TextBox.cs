using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    [SerializeField] private Text text;
    public void ShowText(string textToShow)
    {
        text.text = textToShow;
    }
}
