using UnityEngine;

public class GameFlowService : Singleton<GameFlowService>
{
    [SerializeField] private TextBox textBox;
    [SerializeField] private Day[] days;

    private int currentDayIndex = 0;
    private Day currentDay;

    private void Start()
    {
        currentDay = days[0];
        StartDay(currentDay);
    }

    public void PlayTask()
    {
        textBox.PlayText(currentDay.taskMessage);
        textBox.onTextFinished += TaskFinished;
    }

    private void TaskFinished()
    {
        
    }

    public void PlayActivity()
    {
        textBox.PlayText(currentDay.activityMessage);
        textBox.onTextFinished += ActivtyFinished;
    }

    private void ActivtyFinished()
    {
        textBox.onTextFinished -= ActivtyFinished;
        Debug.Log("activity finished");
    }


    private void StartNextDay()
    {
        currentDayIndex++;
        currentDay = days[currentDayIndex];
        StartDay(currentDay);
    }

    private void StartDay(Day day)
    {
        textBox.PlayText(new[]{day.textMessage});
    }
    
    
}


