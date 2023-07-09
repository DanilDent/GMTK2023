﻿using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ShopListRenderer : MonoSingleton<ShopListRenderer>
{
	private RectTransform _rectTransform;
	[SerializeField] private TextMeshProUGUI _listText;
	[SerializeField] private TextMeshProUGUI _moneyText;
	private float _maxTime;
	private float _timer;
	private float _currentOpacity;
	private bool _rendering;
	private ShopList _currentShopList;
	private Vector2 _startPosition;
	private void Start()
	{
		_rectTransform = GetComponent<RectTransform>();
		_startPosition = new Vector2(_rectTransform.anchoredPosition.x, Screen.height);
		Reset();
	}
	public void Reset()
	{
		_rectTransform.anchoredPosition = _startPosition;
		_timer = 0f;
		_currentOpacity = 0f;
		_listText.color = new Color(1f, 1f, 1f, 0f);
		_moneyText.color = new Color(1f, 1f, 1f, 0f);
	}
	public void Render(ShopList shopList)
	{
		Reset();
		_rendering = true;
		_rectTransform.anchoredPosition = _startPosition;
		_currentShopList = shopList;
		_listText.text = string.Join("\n", shopList.Entries);
		_listText.color = new Color(1f, 1f, 1f, 0f);

		_moneyText.text = shopList.RemainingMoney.ToString();
		_moneyText.color = new Color(1f, 1f, 1f, 0f);
	}
	private float EaseOut(float t)
	{
		return Mathf.Clamp(t * t * (3.0f - 2.0f * t), 0f, 1f);
	}
	private void Update()
	{
		if(_rendering)
		{
			//Debug.Log("Rendering");
			_timer += Time.deltaTime * 1000f;
			var progress = Mathf.Clamp01(_timer / _currentShopList.AnimationTimeMilliSeconds);
			var easedProgress = EaseOut(progress);
			_currentOpacity = easedProgress;
			_rectTransform.anchoredPosition = Vector2.Lerp(_startPosition, Vector2.zero, easedProgress);
			_listText.color = new Color(1f, 1f, 1f, _currentOpacity);
			_moneyText.color = new Color(1f, 1f, 1f, _currentOpacity);
			if(_timer > _currentShopList.AnimationTimeMilliSeconds + _currentShopList.TimeToDisappearMilliSeconds)
			{
				Reset();
				_rendering = false;
			}
		}
	}

}