using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsStore : MonoBehaviour
{
    [SerializeField] private List<News> _BadNews;
    [SerializeField] private List<News> _NeuralNews;
    [SerializeField] private List<News> _GoodNews;

    [SerializeField] private List<GameTime> _cityNewsTimer;
    private List<News> _activeNewsPool;

    [SerializeField] private NewsContainer _newsContainer;

    // Start is called before the first frame update
    private void Start()
    {
        _activeNewsPool = _NeuralNews;
        EventService.Instance.CityStatusChange += ChangeActiveNewsPool;
    }

    private void ChangeActiveNewsPool(int cityState)
    {
        if (cityState > 0)
        {
            _activeNewsPool = _GoodNews;
        }
        else if (cityState < 0)
        {
            _activeNewsPool = _BadNews;
        }
        else
        {
            _activeNewsPool = _NeuralNews;
        }
    }
}