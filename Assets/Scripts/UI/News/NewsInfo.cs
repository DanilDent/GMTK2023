using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class NewsInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questName;
    private News _news;
    private bool _isSuccessed;
    private Image _selfImage;

    // Start is called before the first frame update
    public News News { get => _news; }

    public void Initialize(News news, bool isNewsSuccessed)
    {
        _news = news;
        _selfImage = GetComponent<Image>();
        _isSuccessed = isNewsSuccessed;
        UpdateUIElement();
    }

    private void UpdateUIElement()
    {
        _questName.text = _news.description;
        if (_isSuccessed)
        {
            _selfImage.color = Color.green;
        }
        else
        {
            _selfImage.color = Color.red;
        }
    }

    public void DestoyNewsInfo()
    {
        Destroy(gameObject);
    }
}