using System;
using UnityEngine;

[Serializable]
public struct Chat
{
	[SerializeField] private string _message;
	[SerializeField] private string _firstOption;
	[SerializeField] private string _secondOption;
	[SerializeField] private float _timeToEnd;
	
	public string Message => _message;
	public string FirstOption => _firstOption;
	public string SecondOption => _secondOption;
	public float TimeToEnd => _timeToEnd;
}