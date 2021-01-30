using UnityEngine;

public class GameFlowService : Singleton<GameFlowService>
{
    [Header("References")]
    [SerializeField] private TextBox textBox;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInteractableManager playerInteractableManager;

        [Header("Settings")]
    [SerializeField] private Day[] days;
    [SerializeField] private GameObject[] daysInteractables;

    [SerializeField] private string[] notDoneMessage = {"I'm not tired right now"};

    private int currentDayIndex = 0;
    private Day currentDay;
    
    private bool currentTaskDone = false;
    private bool currentActivityDone = false;
    
    private void Start()
    {
        currentDay = days[0];
        StartDay(currentDay);
        EnableDaysInteractables();
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
        EnableDaysInteractables();
        StartDay(currentDay);
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
    }
}


