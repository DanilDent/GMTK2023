using System;
using UnityEngine;

public class WinConditionRenderer : MonoBehaviour
{
	[SerializeField] private GameObject _winPanel;
	[SerializeField] private GameObject _losePanel;
	private void Start()
	{
		_winPanel.SetActive(false);
		_losePanel.SetActive(false);
		SubscribeToEvents();
	}
	private void OnDestroy()
	{
		UnsubscribeFromEvents();
	}
	private void SubscribeToEvents()
	{
		EventService.Instance.Victory += RenderWin;
		EventService.Instance.Defeat += RenderLose;
	}
	private void UnsubscribeFromEvents()
	{
		EventService.Instance.Victory -= RenderWin;
		EventService.Instance.Defeat -= RenderLose;
	}
	public void RenderWin()
	{
		_winPanel.SetActive(true);
	}
	public void RenderLose()
	{
		_losePanel.SetActive(true);
	}
}