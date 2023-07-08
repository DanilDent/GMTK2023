using UnityEngine;

public class ChatConfig : ScriptableObject
{
	[SerializeField] private Chat[] _data;

	public Chat[] GetData => _data;
}