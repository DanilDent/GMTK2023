using System.Collections.Generic;
using UnityEngine;

public class NewsContainer : MonoBehaviour
{
    [SerializeField] private NewsInfo _newsPrefab;

    [SerializeField] private int _maxDisplayedNewsCount = 3;
    [SerializeField] private Queue<NewsInfo> _news;

    // Start is called before the first frame update
    private void Start()
    {
        _news = new Queue<NewsInfo>();
        EventService.Instance.QuestCompleted += AddNewsToDisplay;
    }

    public void AddCityNewsToDisplay(News news)
    {
        NewsInfo createdNews = Instantiate(_newsPrefab, transform.position, Quaternion.identity);
        createdNews.transform.SetParent(transform, false);
        createdNews.transform.SetAsFirstSibling();
        createdNews.Initialize(news, NewsInfo.NewsType.City);
        _news.Enqueue(createdNews);
        if (_news.Count > _maxDisplayedNewsCount)
        {
            NewsInfo deletedNews = _news.Dequeue();
            deletedNews.DestoyNewsInfo();
        }
    }

    private void AddNewsToDisplay(Quest quest, bool isQuestSuccesed)
    {
        var news = isQuestSuccesed ? quest.SuccessfulNews : quest.FailureNews;
        var newsType = isQuestSuccesed ? NewsInfo.NewsType.Good : NewsInfo.NewsType.Bad;

        NewDayManager.Instance.NewsCommands.Enqueue
        (
            new NewDayCommand { CmdType = NewDayCmdType.DisplayNews, News = new ImportantNews { News = news, NewsType = newsType } }
         );

        //NewsInfo createdNews = Instantiate(_newsPrefab, transform.position, Quaternion.identity);
        //createdNews.transform.SetParent(transform, false);
        //createdNews.transform.SetAsFirstSibling();
        //if (isQuestSuccesed)
        //{
        //    createdNews.Initialize(quest.SuccessfulNews, NewsInfo.NewsType.Good);
        //}
        //else
        //{
        //    createdNews.Initialize(quest.FailureNews, NewsInfo.NewsType.Bad);
        //}

        //_news.Enqueue(createdNews);

        //if (_news.Count > _maxDisplayedNewsCount)
        //{
        //    NewsInfo deletedNews = _news.Dequeue();
        //    deletedNews.DestoyNewsInfo();
        //}
    }
}