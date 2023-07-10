using UnityEngine;
using UnityEngine.UI;

public class ImportatnNewsInfo : NewsInfo
{
    [SerializeField] protected Sprite _goodNewsBackground;
    [SerializeField] protected Sprite _badNewsBackground;

    public override void Initialize(News news, NewsType newsType)
    {
        _news = news;
        _selfImage = GetComponent<Image>();
        _newsType = newsType;
        UpdateUIElement();
    }

    protected override void UpdateUIElement()
    {
        _questName.text = _news.description;
        _selfImage.sprite = _newsType == NewsType.Good ? _goodNewsBackground : _badNewsBackground;
    }
}