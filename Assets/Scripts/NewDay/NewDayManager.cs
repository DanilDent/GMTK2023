using System.Collections.Generic;

public class NewDayManager : MonoSingleton<NewDayManager>
{
    public Queue<NewDayCommand> NewsCommands;
    public Queue<NewDayCommand> NewDayCommands;

    protected override void Awake()
    {
        base.Awake();
        NewsCommands = new Queue<NewDayCommand>();
        NewDayCommands = new Queue<NewDayCommand>();
    }

    private void Start()
    {
        EventService.Instance.QuestLifetimeEnded += HandleQuestLifetimeEnded;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventService.Instance.QuestLifetimeEnded -= HandleQuestLifetimeEnded;
    }

    public void ExecuteNewsCommands()
    {
        NewDayWindowUIView.Instance.ClearContainer();
        while (NewsCommands.Count > 0)
        {
            var cmd = NewsCommands.Dequeue();
            NewDayWindowUIView.Instance.AddNews(cmd.News);
        }
    }

    public void ExecuteNewDayCommands()
    {
        while (NewDayCommands.Count > 0)
        {
            var cmd = NewDayCommands.Dequeue();
            if (cmd.CmdType == NewDayCmdType.SwitchMusic)
            {
                SoundService.Instance.SetClip(cmd.Audio);
            }
            else if (cmd.CmdType == NewDayCmdType.SwitchBackground)
            {
                UIManager.Instance.UpdateBackground(cmd.Background, cmd.BackgroundNpc);
            }
        }

        SoundService.Instance.Play(2f);
    }

    private void HandleQuestLifetimeEnded(Quest quest)
    {
        if (quest.FailureNews.description == string.Empty)
        {
            return;
        }
        NewDayCommands.Enqueue(new NewDayCommand
        {
            CmdType = NewDayCmdType.DisplayNews,
            News = new ImportantNews { News = quest.FailureNews, NewsType = NewsInfo.NewsType.Bad }
        });
    }
}

