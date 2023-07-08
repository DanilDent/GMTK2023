using System;

public class EventService
{
    // Singleton Impl
    protected EventService() { }

    private static EventService _instance;
    public static EventService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventService();
            }

            return _instance;
        }
    }

    // Events
    public Action<QuestConfig> NewQuestBecomeAvailable;
    // Passes current GameTime
    public Action<GameTime> GameTimeUpdated;
}


