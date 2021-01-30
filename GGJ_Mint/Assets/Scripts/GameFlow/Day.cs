using UnityEngine;

[CreateAssetMenu(fileName = "Day", menuName = "ScriptableObjects/Day", order = 1)]
public class Day : ScriptableObject
{
    public string name;
    public string textMessage;
    public string[] prompt;
    public string[] answers;

    public string[] taskMessage;
    public string taskSound;
    public string[] activityMessage;
    public string activitySound;

    public string dayMusic;
}
