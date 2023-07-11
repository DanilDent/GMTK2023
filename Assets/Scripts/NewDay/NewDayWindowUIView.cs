using UnityEngine;
using UnityEngine.UI;

public class NewDayWindowUIView : MonoSingleton<NewDayWindowUIView>
{
    [SerializeField] private RectTransform _newsContainer;
    [SerializeField] private NewsInfo _newsPrefab;
    [SerializeField] private Button _okBtn;

    private void Start()
    {
        _okBtn.onClick.AddListener(OnOkBtnClick);
    }

    protected override void OnDestroy()
    {
        _okBtn.onClick.RemoveAllListeners();
    }

    private void OnEnable()
    {
        PlayerCursorBehaviour.Instance.LockDiagArea = false;
    }

    private void OnDisable()
    {
        PlayerCursorBehaviour.Instance.LockDiagArea = true;
        _newsContainer.gameObject.SetActive(false);
        ClearContainer();
    }

    public void AddNews(ImportantNews news)
    {
        var newsInfo = Instantiate(_newsPrefab, _newsContainer.transform);
        newsInfo.Initialize(news.News, news.NewsType);
    }

    public void ClearContainer()
    {
        for (int i = 0; i < _newsContainer.transform.childCount; ++i)
        {
            var child = _newsContainer.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public void ShowNews()
    {
        _newsContainer.gameObject.SetActive(true);
    }

    private void OnOkBtnClick()
    {
        EventService.Instance.NextDay?.Invoke();
        gameObject.SetActive(false);
    }
}