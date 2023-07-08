using UnityEngine;

[CreateAssetMenu(fileName = "New Game Timeline", menuName = "Configs/Game Timeline")]
public class GameTimelineConfig
    : ScriptableObject
{
    public int SecRealTimeToMinsGameTime => _secRealTimeToMinsGameTime;
    public int GameTimeStepChange => _gameTimeStepChange;
    public TimelineEventData[] Timeline => _timeline;

    [SerializeField] private TimelineEventData[] _timeline;
    [SerializeField] private int _secRealTimeToMinsGameTime = 3;
    [SerializeField] private int _gameTimeStepChange = 10;
}


