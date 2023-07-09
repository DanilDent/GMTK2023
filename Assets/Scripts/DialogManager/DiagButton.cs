using System.Collections;
using System.Threading.Tasks;
using GitIntegration.ShopSystems;
using UnityEngine;
using UnityEngine.UI;

public class DiagButton : MonoBehaviour
{
	public ButtonType BtnType;

	[SerializeField] private Button _btn;

	private void Start()
	{
		_btn.onClick.AddListener(HandleOnClick);
		EventService.Instance.DiagButtonClickedByBot += HandleDiagBtnPressedByBot;
	}

	private void OnDestroy()
	{
		_btn.onClick.RemoveListener(HandleOnClick);
		EventService.Instance.DiagButtonClickedByBot -= HandleDiagBtnPressedByBot;
	}

	private void HandleOnClick()
	{
		EventService.Instance.DiagButtonClicked?.Invoke(BtnType);
	}

	private void HandleDiagBtnPressedByBot(ButtonType btnType)
	{
		if(btnType == BtnType)
		{
			StartCoroutine(AnimCoroutine());
		}
	}

	private void OnDisable()
	{
		_btn.interactable = true;
	}

	private IEnumerator AnimCoroutine()
	{
		_btn.interactable = false;
		yield return new WaitForSeconds(0.2f);
		_btn.interactable = true;
		var diagManager = DialogManager.Instance;
		switch(BtnType)
		{
			case ButtonType.Skip:
				diagManager.DisplayMain();
				break;
			case ButtonType.Next:
				diagManager.DisplayMain();
				break;
			case ButtonType.Shop:
				diagManager.DisplayEmpty();
				//sorry for this
				ShopListRenderer.Instance.OnRenderComplete += OnShopRenderComplete;
				break;
			case ButtonType.Talk:
				diagManager.DisplayTalk();
				break;
			case ButtonType.GetQuest:
				break;
			case ButtonType.Exit:
				break;
			case ButtonType.AnsA:
				diagManager.DisplayMain();
				break;
			case ButtonType.AnsB:
				diagManager.DisplayMain();
				break;
			case ButtonType.Back:
				break;
		}
	}

	private void OnShopRenderComplete()
	{
		ShopListRenderer.Instance.OnRenderComplete -= OnShopRenderComplete;
		DialogManager.Instance.DisplayShop();
	}
}