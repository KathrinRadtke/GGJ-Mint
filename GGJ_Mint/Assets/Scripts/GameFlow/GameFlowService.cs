using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameFlowService : Singleton<GameFlowService>
{
    [Header("References")]
    [SerializeField] private TextBox textBox;

    [SerializeField] private Image fadeScreen;
    [SerializeField] private Volume ppVolume;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteractableManager playerInteractableManager;
    [SerializeField] private Animator dayTextAnimator;
    [SerializeField] private Text dayText;
    [SerializeField] private GameObject creditsScreen;

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

    [SerializeField] private Interactable bed;
    
    public void StartGame()
    {
        StartCoroutine(FadeAndStartGame());
    }

    private IEnumerator FadeAndStartGame()
    {
        currentDay = days[0];

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

        dayTextAnimator.SetTrigger("show");
        dayText.text = currentDay.name;
        fadeTimer = 0;
        while (fadeTime > fadeTimer)
        {
            fadeTimer += Time.deltaTime;
            fadeScreen.color = Color.Lerp(Color.black, new Color(0,0,0, 0), fadeTimer/fadeTime);
            yield return null;
        }
        fadeScreen.color = new Color(0,0,0,0);
       
        
        yield return new WaitForSeconds(fadeTime);
        dayTextAnimator.SetTrigger("hide");
        
        StartDay(currentDay);
    }

    public void SetMovementAndInteraction(bool setEnabled)
    {
        if (!setEnabled) playerMovement.StopWalkAnimation();
        playerMovement.enabled = setEnabled;
        playerInteractableManager.enabled = setEnabled;
    }

    public IEnumerator FadeEndScreen()
    {
        yield return new WaitForSeconds(2f);
        float fadeTimer = 0;

        while (fadeTime > fadeTimer)
        {
            fadeTimer += Time.deltaTime;
            fadeScreen.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, fadeTimer / fadeTime);
            yield return null;
        }

        yield return new WaitForSeconds(blackoutTime);
        creditsScreen.SetActive(true);

        fadeTimer = 0;
        while (fadeTime > fadeTimer)
        {
            fadeTimer += Time.deltaTime;
            fadeScreen.color = Color.Lerp(Color.black, new Color(0, 0, 0, 0), fadeTimer / fadeTime);
            yield return null;
        }
        fadeScreen.color = new Color(0, 0, 0, 0);
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

        if (currentTaskDone && currentActivityDone)
        {
            HighlightBed();
        }
    }

    public void PlayActivity()
    {
        if (!currentActivityDone)
        {
            textBox.PlayText(currentDay.activityMessage);
            textBox.onTextFinished += ActivtyFinished;
            SoundManager.Instance.playSound(currentDay.activitySound);
        } if (currentDayIndex == 6)
        {
            // Show end screen
            StartCoroutine(FadeEndScreen());
        }
    }

    private void ActivtyFinished()
    {
        textBox.onTextFinished -= ActivtyFinished;
        currentActivityDone = true;
        
        if (currentTaskDone && currentActivityDone)
        {
            HighlightBed();
        }
    }

    private void HighlightBed()
    {
        bed.EnableSparcles();
    }

    public void GoToBed()
    {
        if (currentTaskDone && currentActivityDone)
        {
            SoundManager.Instance.playSound("Bed");
            StartNextDay();
        }
        else
        {
           // textBox.PlayText(notDoneMessage);
        }
    }

    private void StartNextDay()
    {
        currentDayIndex++;
        currentTaskDone = false;
        currentActivityDone = false;
        currentDay = days[currentDayIndex];
        StartCoroutine(FadeAndStartNextDay());
        IncreaseSaturation();
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
        
        dayTextAnimator.SetTrigger("show");
        dayText.text = currentDay.name;

        fadeTimer = 0;
        while (fadeTime > fadeTimer)
        {
            fadeTimer += Time.deltaTime;
            fadeScreen.color = Color.Lerp(Color.black, new Color(0,0,0, 0), fadeTimer/fadeTime);
            yield return null;
        }
        fadeScreen.color = new Color(0,0,0,0);
        
        yield return new WaitForSeconds(fadeTime);
        dayTextAnimator.SetTrigger("hide");

        SetMovementAndInteraction(true);
        StartDay(currentDay);
    }

    private void EnableDaysInteractables()
    {
        if (currentDayIndex > 0)
        {
            Interactable[] interactables =
                daysInteractables[currentDayIndex - 1].GetComponentsInChildren<Interactable>();
            foreach (var interactable in interactables)
            {
                interactable.Disable();
            }
        }

        daysInteractables[currentDayIndex].SetActive(true);
    }

    private void StartDay(Day day)
    {
        SoundManager.Instance.playMusic(day.dayMusic, 1f);
        SoundManager.Instance.playSound("CellphoneVibrate");
        textBox.PlayText(new[]{day.textMessage}, TextBox.TextType.Phone);
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


    private void IncreaseSaturation()
    {
        ColorAdjustments colorAdjustments;
        ppVolume.profile.TryGet(out colorAdjustments);
        colorAdjustments.saturation.value += 7f;
    }
}


