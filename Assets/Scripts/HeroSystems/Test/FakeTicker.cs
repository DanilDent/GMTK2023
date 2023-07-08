using System;
using UnityEngine;

public class FakeTicker : MonoBehaviour
{
	public GameTime TickGameTime;
	public GameTime CurrentTime{get; private set;}
	private float _time;
	private float _tickTime;
	private void Awake()
	{
		CurrentTime = new GameTime(0, new Vector2Int(0, 0));
	}
	private void Update()
	{
		_time += Time.deltaTime;
		if(_time >= _tickTime)
		{
			_time = 0;
			CurrentTime += TickGameTime;
		}
	}
}