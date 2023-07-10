using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ShopListRenderer : MonoSingleton<ShopListRenderer>
{
    private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _listText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private RectTransform _heroMoneyLabel;


    public event Action OnRenderComplete;

    private float _maxTime;
    private float _timer;
    private float _currentOpacity;
    private bool _rendering;
    private ShopList _currentShopList;
    private Vector2 _startPosition;

    private Color _initColor;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        var anchoredPosition = _rectTransform.anchoredPosition;
        _startPosition = new Vector2(anchoredPosition.x, anchoredPosition.y);
        _initColor = _listText.color;

        Reset();
    }
    public bool IsRendering()
    {
        return _rendering;
    }
    public void Reset()
    {
        _rectTransform.anchoredPosition = _startPosition;
        _timer = 0f;
        _currentOpacity = 0f;
        _rendering = false;
        _listText.color = new Color(_initColor.r, _initColor.g, _initColor.b, 0f);
        _moneyText.color = new Color(_initColor.r, _initColor.g, _initColor.b, 0f);
    }
    public void Render(ShopList shopList)
    {
        Reset();
        _rendering = true;
        _heroMoneyLabel.gameObject.SetActive(true);
        _rectTransform.anchoredPosition = _startPosition;
        _currentShopList = shopList;
        _listText.text = string.Join("\n", shopList.Entries);
        _listText.color = new Color(_initColor.r, _initColor.g, _initColor.b, 0f);

        _moneyText.text = shopList.RemainingMoney.ToString();
        _moneyText.color = new Color(_initColor.r, _initColor.g, _initColor.b, 0f);
    }
    private float EaseOut(float t)
    {
        return Mathf.Clamp(t * t * (3.0f - 2.0f * t), 0f, 1f);
    }
    private void Update()
    {
        if (_rendering)
        {
            //Debug.Log("Rendering");
            _timer += Time.deltaTime * 1000f;
            var progress = Mathf.Clamp01(_timer / _currentShopList.AnimationTimeMilliSeconds);
            var easedProgress = EaseOut(progress);
            _currentOpacity = easedProgress;
            _rectTransform.anchoredPosition = Vector2.Lerp(_startPosition, Vector2.zero, easedProgress);
            _listText.color = new Color(_initColor.r, _initColor.g, _initColor.b, _currentOpacity);
            _moneyText.color = new Color(_initColor.r, _initColor.g, _initColor.b, _currentOpacity);
            if (_timer > _currentShopList.AnimationTimeMilliSeconds + _currentShopList.TimeToDisappearMilliSeconds)
            {
                Reset();
                OnRenderComplete?.Invoke();
                _rendering = false;
                _heroMoneyLabel.gameObject.SetActive(false);

            }
        }
    }

}