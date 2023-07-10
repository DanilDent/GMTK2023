using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiagButton : MonoBehaviour
{
    public ButtonType BtnType;
    [SerializeField] private Sprite _btnPressed;
    [SerializeField] private Sprite _btnNormal;

    [SerializeField] private Button _btn;
    private Image _btnImg;
    IEnumerator curCoroutine;

    private void Start()
    {
        _btnImg = GetComponent<Image>();
        _btn.onClick.AddListener(HandleOnClick);
        EventService.Instance.DiagButtonClickedByBot += HandleDiagButtonClickedByBot;
    }

    private void OnDestroy()
    {
        _btn.onClick.RemoveListener(HandleOnClick);
        EventService.Instance.DiagButtonClickedByBot -= HandleDiagButtonClickedByBot;
    }

    private void HandleOnClick()
    {
        if (BtnType == ButtonType.GetQuest)
        {
            PlayerCursorBehaviour.Instance.LockDiagArea = false;
        }
        EventService.Instance.DiagButtonClicked?.Invoke(BtnType);
    }

    private void HandleDiagButtonClickedByBot(ButtonType btnType)
    {
        if (btnType == BtnType)
        {
            if (gameObject.activeInHierarchy)
            {
                curCoroutine = DiagBtnClickedByBotCoroutine();
                StartCoroutine(curCoroutine);
            }
        }
    }

    private void OnDisable()
    {
        if (curCoroutine != null)
        {
            StopCoroutine(curCoroutine);
            curCoroutine = null;
        }
    }

    private IEnumerator DiagBtnClickedByBotCoroutine()
    {
        _btnImg.sprite = _btnPressed;
        yield return new WaitForSeconds(0.2f);
        _btnImg.sprite = _btnNormal;
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