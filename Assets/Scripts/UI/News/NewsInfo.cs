using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewsInfo : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _questName;
    protected News _news;
    protected NewsType _newsType;
    protected Image _selfImage;

    // Start is called before the first frame update
    public News News { get => _news; }

    public enum NewsType
    {
        City,
        Bad,
        Good
    }

    public virtual void Initialize(News news, NewsType newsType)
    {
        _news = news;
        _selfImage = GetComponent<Image>();
        _newsType = newsType;
        UpdateUIElement();
    }

    protected virtual void UpdateUIElement()
    {
        _questName.text = _news.description;
        switch (_newsType)
        {
            case NewsType.City:
                _selfImage.color = Color.cyan;
                break;

            case NewsType.Bad:
                _selfImage.color = Color.red;
                break;

            case NewsType.Good:
                _selfImage.color = Color.green;
                break;
        }
    }

    public void DestoyNewsInfo()
    {
        Destroy(gameObject);
    }
}