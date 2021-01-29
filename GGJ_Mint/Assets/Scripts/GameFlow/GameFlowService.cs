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
        
    }

    public void PlayActivity()
    {
        
    }


    private void StartNextDay()
    {
        currentDayIndex++;
        currentDay = days[currentDayIndex];
        StartDay(currentDay);
    }

    private void StartDay(Day day)
    {
        textBox.ShowText(day.textMessage);
    }
    
    
}


