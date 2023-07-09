using System.Collections;
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

    private void AddNewsToDisplay(Quest quest, bool isQuestSuccesed)
    {
        NewsInfo createdNews = Instantiate(_newsPrefab, transform.position, Quaternion.identity);
        createdNews.transform.parent = transform;
        createdNews.transform.SetAsFirstSibling();
        if (isQuestSuccesed)
        {
            createdNews.Initialize(quest.SuccessfulNews, isQuestSuccesed);
        }
        else
        {
            createdNews.Initialize(quest.FailureNews, isQuestSuccesed);
        }

        _news.Enqueue(createdNews);

        if (_news.Count > _maxDisplayedNewsCount)
        {
            NewsInfo deletedNews = _news.Dequeue();
            deletedNews.DestoyNewsInfo();
        }
    }
}