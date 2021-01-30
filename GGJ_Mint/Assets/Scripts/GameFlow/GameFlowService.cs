using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameFlowService : Singleton<GameFlowService>
{
    [Header("References")]
    [SerializeField] private TextBox textBox;

    [SerializeField] private Image fadeScreen;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteractableManager playerInteractableManager;

        [Header("Settings")]
    [SerializeField] private Day[] days;
    [SerializeField] private GameObject[] daysInteractables;
    [SerializeField] private float fadeTime = 0.5f;
    [SerializeField] private float blackoutTime = 0.5f;

    [SerializeField] private string[] notDoneMessage = {"I'm not tired right now"};

    private int currentDayIndex = 0;
    private Day currentDay;
    
    private bool currentTaskDone = false;
    private bool currentActivityDone = false;
    
    public void StartGame()
    {
        StartCoroutine(FadeAndStartGame());
    }

    private IEnumerator FadeAndStartGame()
    {
        yield return new WaitForSeconds(0.5f);
        float fadeTimer = 0;
        
        while (fadeTime > fadeTimer)
        {
            fadeTimer += Time.deltaTime;
            fadeScreen.color = Color.Lerp(new Color(0,0,0, 0), Color.black, fadeTimer/fadeTime);
            yield return null;
        }
        EnableDaysInteractables();
        yield return new WaitForSeconds(blackoutTime);

        fadeTimer = 0;
        while (fadeTime > fadeTimer)
        {
            fadeTimer += Time.deltaTime;
            fadeScreen.color = Color.Lerp(Color.black, new Color(0,0,0, 0), fadeTimer/fadeTime);
            yield return null;
        }
        fadeScreen.color = new Color(0,0,0,0);
        currentDay = days[0];
        StartDay(currentDay);
    }

    public void SetMovementAndInteraction(bool setEnabled)
    {
        playerMovement.enabled = setEnabled;
        playerInteractableManager.enabled = setEnabled;
    }

    public void PlayTask()
    {
        if (!currentTaskDone)
        {
            textBox.PlayText(currentDay.taskMessage);
            textBox.onTextFinished += TaskFinished;
            SoundManager.Instance.playSound(currentDay.taskSound);
        }
    }

    private void TaskFinished()
    {
        textBox.onTextFinished -= TaskFinished;
        currentTaskDone = true;
    }

    public void PlayActivity()
    {
        if (!currentActivityDone)
        {
            textBox.PlayText(currentDay.activityMessage);
            textBox.onTextFinished += ActivtyFinished;
            SoundManager.Instance.playSound(currentDay.activitySound);
        }
    }

    private void ActivtyFinished()
    {
        textBox.onTextFinished -= ActivtyFinished;
        currentActivityDone = true;
    }

    public void GoToBed()
    {
        if (currentTaskDone && currentActivityDone)
        {
            StartNextDay();
        }
        else
        {
            textBox.PlayText(notDoneMessage);
        }
    }

    private void StartNextDay()
    {
        currentDayIndex++;
        currentTaskDone = false;
        currentActivityDone = false;
        currentDay = days[currentDayIndex];
        StartCoroutine(FadeAndStartNextDay());
    }

    private IEnumerator FadeAndStartNextDay()
    {
        SetMovementAndInteraction(false);
        float fadeTimer = 0;
        while (fadeTime > fadeTimer)
        {
            fadeTimer += Time.deltaTime;
            fadeScreen.color = Color.Lerp(new Color(0,0,0, 0), Color.black, fadeTimer/fadeTime);
            yield return null;
        }
        yield return new WaitForSeconds(blackoutTime);
        EnableDaysInteractables();

        fadeTimer = 0;
        while (fadeTime > fadeTimer)
        {
            fadeTimer += Time.deltaTime;
            fadeScreen.color = Color.Lerp(Color.black, new Color(0,0,0, 0), fadeTimer/fadeTime);
            yield return null;
        }
        fadeScreen.color = new Color(0,0,0,0);

        StartDay(currentDay);
        SetMovementAndInteraction(true);
    }

    private void EnableDaysInteractables()
    {
        foreach (GameObject gameObject in daysInteractables)
        {
            gameObject.SetActive(false);
        }
        daysInteractables[currentDayIndex].SetActive(true);
    }

    private void StartDay(Day day)
    {
        textBox.PlayText(new[]{day.textMessage});
        if (currentDay.prompt != null && currentDay.prompt.Length > 0)
        {
            textBox.onTextFinished += PlayPrompt;
        }
    }

    private void PlayPrompt()
    {
        textBox.onTextFinished -= PlayPrompt;
        textBox.PlayText(currentDay.prompt);
        if (currentDay.answers != null)
        {
            textBox.onTextFinished += PlayAnswers;
        }

        if ((currentDay.taskMessage == null || currentDay.taskMessage.Length == 0) &&
            (currentDay.activityMessage == null || currentDay.activityMessage.Length == 0))
        {
            textBox.onTextFinished += SkipDay;
        }
    }

    private void SkipDay()
    {
        textBox.onTextFinished -= SkipDay;
        StartNextDay();

    }

    private void PlayAnswers()
    {
        // todo
        textBox.onTextFinished -= PlayAnswers;
    }
}


