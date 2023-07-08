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
  public Action<Quest> NewQuestBecomeAvailable;
    // Passes current GameTime
=======
    // Passes current game time
    public Action<GameTime> GameTimeUpdated;
    public Action<OnHeroMoodChangedEventArgs> HeroMoodChanged;
    public Action<Quest> QuestAssigned;
    public Action<Quest, bool> QuestCompleted;

}


