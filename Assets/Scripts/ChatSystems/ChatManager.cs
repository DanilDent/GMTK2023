using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
	[SerializeField] private ChatConfig _chatConfig;
	public ChatManager Instance{get; private set;}
	private List<Chat> _chats;
	private void Start()
	{
		_chats = _chatConfig.GetData.ToList();
		SubscribeToEvents();
	}
	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this);
		}
	}
	public Chat ChooseRandomChat()
	{
		var randomIndex = UnityEngine.Random.Range(0, _chats.Count);
		return _chats[randomIndex];
	}
	private void OnDestroy()
	{
		UnsubscribeFromEvents();
	}
	private void SubscribeToEvents()
	{
		EventService.Instance.QuestAssigned += OnQuestAssigned;
	}
	private void UnsubscribeFromEvents()
	{
		EventService.Instance.QuestAssigned -= OnQuestAssigned;
	}
	private void OnQuestAssigned(Quest obj)
	{
		ProcessChat();
	}
	private async void ProcessChat()
	{
		var chat = ChooseRandomChat();
		var textBox = UIManager.Instance.ActionsPanel.ChangeToTextBoxAndGet();
		var delay = chat.TimeToEnd / chat.Message.Length;
		await PrintTextSlowly(chat.Message, delay, textBox);
	}
	/*private Task<bool>  PrintTextSlowly(string text, float delay, TMP_Text textBox)
	{
		foreach(var ch in text)
		{
			textBox.text += ch;
			await Task.Delay(TimeSpan.FromSeconds(delay));
		}
		
	}*/
}