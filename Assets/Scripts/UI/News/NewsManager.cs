using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    public static NewsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [SerializeField] private List<News> _BadNews;
    [SerializeField] private List<News> _NeuralNews;
    [SerializeField] private List<News> _GoodNews;

    [SerializeField] private List<GameTime> _cityNewsTime;
    private List<News> _activeNewsPool;

    [SerializeField] private NewsContainer _newsContainer;

    // Start is called before the first frame update

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }

        EventService.Instance.GameTimeUpdated -= OnGameTimeUpdate;
    }

    private void Start()
    {
        _activeNewsPool = _NeuralNews;
        EventService.Instance.CityStatusChange += ChangeActiveNewsPool;
        EventService.Instance.GameTimeUpdated += OnGameTimeUpdate;
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

    public void OnGameTimeUpdate()
    {
        CheckNewsTime();
    }

    private void CheckNewsTime()
    {
        foreach (GameTime gameTime in _cityNewsTime)
        {
            Debug.Log(gameTime.ToString());
            if (gameTime <= GameManager.Instance.CurrentTime)
            {
                int randomNewsIndex = Random.Range(0, _activeNewsPool.Count - 1);
                _newsContainer.AddCityNewsToDisplay(_activeNewsPool[randomNewsIndex]);
                _activeNewsPool.Remove(_activeNewsPool[randomNewsIndex]);
                _cityNewsTime.Remove(gameTime);
                return;
            }
        }
    }
}