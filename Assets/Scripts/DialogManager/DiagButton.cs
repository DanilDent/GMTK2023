using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiagButton : MonoBehaviour
{
    public ButtonType BtnType;

    [SerializeField] private Button _btn;

    private void Start()
    {
        _btn.onClick.AddListener(HandleOnClick);
        EventService.Instance.DiagBtnClickedByBot += HandleDiagButtonClickedByBot;
    }

    private void OnDestroy()
    {
        _btn.onClick.RemoveListener(HandleOnClick);
        EventService.Instance.DiagBtnClickedByBot -= HandleDiagButtonClickedByBot;
    }

    private void HandleOnClick()
    {
        EventService.Instance.DiagButtonClicked?.Invoke(BtnType);
    }

    private void HandleDiagButtonClickedByBot(ButtonType btnType)
    {
        if (btnType == BtnType)
        {
            StartCoroutine(DiagBtnClickedByBotCoroutine());
        }
    }

    private void OnDisable()
    {
        _btn.interactable = true;
    }

    private IEnumerator DiagBtnClickedByBotCoroutine()
    {
        _btn.interactable = false;
        yield return new WaitForSeconds(0.2f);
        _btn.interactable = true;
        _btn.onClick?.Invoke();
        //switch (BtnType)
        //{
        //    case ButtonType.Skip:
        //        diagManager.DisplayMain();
        //        break;
        //    case ButtonType.Next:
        //        diagManager.DisplayMain();
        //        break;
        //    case ButtonType.Shop:
        //        diagManager.DisplayShop();
        //        break;
        //    case ButtonType.Talk:
        //        diagManager.DisplayTalk();
        //        break;
        //    case ButtonType.GetQuest:
        //        EventService.Instance.GetQuesDiagBtnClicked?.Invoke();
        //        break;
        //    case ButtonType.Exit:
        //        _btn.onClick?.Invoke();
        //        break;
        //    case ButtonType.AnsA:
        //        diagManager.DisplayMain();
        //        break;
        //    case ButtonType.AnsB:
        //        diagManager.DisplayMain();
        //        break;
        //    case ButtonType.Back:
        //        break;
        //}
    }
}