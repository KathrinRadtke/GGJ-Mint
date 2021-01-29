using UnityEngine;

[CreateAssetMenu(fileName = "Day", menuName = "ScriptableObjects/Day", order = 1)]
public class Day : ScriptableObject
{
    public string textMessage;
    public string prompt;
    public string[] answers;

    public string taskMessage;
    public string activityMessage;
}
