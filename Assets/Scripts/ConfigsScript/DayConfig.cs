using UnityEngine;

[CreateAssetMenu(fileName = "New Day Config", menuName = "Configs/Day Config")]
public class DayConfig : ScriptableObject
{
    public GameTime StartOfDay => _startOfDay;
    public GameTime EndOfDay => _endOfDay;
    public TimelineEventData[] Timeline => _timeline;

    [SerializeField] private GameTime _startOfDay;
    [SerializeField] private GameTime _endOfDay;
    [SerializeField] private TimelineEventData[] _timeline;
}