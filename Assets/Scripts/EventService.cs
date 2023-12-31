using System;

public class EventService
{
    // Singleton Impl
    protected EventService()
    {
    }

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
    public Action<string> NewHeroComing;

    public Action HeroLeaving;
    public Action HeroLeftFromScreen;

    public Action<Quest> NewQuestBecomeAvailable;

    public Action GameTimeUpdated;

    public Action<OnHeroMoodChangedEventArgs> HeroMoodChanged;

    public Action<Quest> QuestAssigned;

    public Action<Quest, bool> QuestCompleted;

    public Action<Quest> QuestBecomeAvailableToGiveHero;

    public Action<Quest> QuestLifetimeEnded;

    public Action CityDestroyed;
    public Action<float> CityHealthChanged;
    public Action<float> CityHealthChange;
    public Action<ButtonType> DiagButtonClicked;
    public Action<ButtonType> DiagButtonClickedByBot;
    public Action<int> CityStatusChange;
    public Action GetQuesDiagBtnClicked;
    public Action ExitDiagBtnClicked;
    public Action Victory;
    public Action Defeat;

    public Action DayEnd;
    public Action NextDay;

    public Action StartPlay;
}