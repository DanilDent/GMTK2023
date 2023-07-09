using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class DialogManager : MonoSingleton<DialogManager>
{
    [SerializeField] private HelloPhrasesConfig helloPhrasesConfig;
    [SerializeField] private DialogConfig dialogConfig;
    // Btns
    //[SerializeField] private Button _skipBtn; // 0
    //[SerializeField] private Button _nextBtn; // 1
    //[SerializeField] private Button _shopBtn; // 2
    //[SerializeField] private Button _talkBtn; // 3
    //[SerializeField] private Button _getQuestBtn; // 4
    //[SerializeField] private Button _exitBtn; // 5
    //[SerializeField] private Button _ansABtn; // 6
    //[SerializeField] private Button _ansBBtn; // 7
    //[SerializeField] private Button _backBtn; // 8
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private Button[] _btns;


    private string[] helloPhrases;
    private float allDurationMessage;
    private IEnumerator _dispDiagCoroutine;
    private IEnumerator _displayNextButtonWithDelay;

    void Start()
    {
        helloPhrases = helloPhrasesConfig.Data;
        allDurationMessage = dialogConfig.AllDurationMessage;

        EventService.Instance.DiagButtonClicked += HandleDiagBbtnClicked;
    }

    protected override void OnDestroy()
    {
        EventService.Instance.DiagButtonClicked -= HandleDiagBbtnClicked;
    }

    void Update()
    {

    }

    public void UpdateDialogueText(string text)
    {
        _dialogueText.text = string.Empty;
        if (_dispDiagCoroutine != null)
        {
            StopCoroutine(_dispDiagCoroutine);
            _dispDiagCoroutine = null;
        }
        _dispDiagCoroutine = DisplayDialogueTextCoroutine(text);
        StartCoroutine(_dispDiagCoroutine);
    }

    public void DisplayBlank()
    {
        UpdateDialogueText(String.Empty);
        Display();
    }

    public void DisplayHello()
    {
        UpdateDialogueText(helloPhrases[Random.Range(0, helloPhrases.Length)]);
        Display(0);
        _displayNextButtonWithDelay = InvokeWithDelay(() => { Display(1); }, allDurationMessage + .5f);
        StartCoroutine(_displayNextButtonWithDelay);
    }

    public void DisplayMain()
    {
        UpdateDialogueText("What do you want to do?");
        Display(2, 3, 4, 5);
    }

    public void DisplayShop()
    {
        UpdateDialogueText("Shop feedback message will displayed here soon");
        Display(5);
    }

    public void DisplayTalk()
    {
        UpdateDialogueText("Some question here");
        Display(6, 7);
    }

    private void Display(params int[] btnIndecies)
    {
        for (int i = 0; i < _btns.Length; ++i)
        {
            _btns[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < btnIndecies.Length; ++i)
        {
            _btns[btnIndecies[i]].gameObject.SetActive(true);
        }
    }

    private IEnumerator DisplayDialogueTextCoroutine(string message)
    {
        int charIndex = 0;

        while (charIndex < message.Length)
        {
            _dialogueText.text += message[charIndex];
            ++charIndex;
            float waitFor = (float)allDurationMessage / message.Length;
            yield return new WaitForSeconds(waitFor);
        }
    }

    private void HandleDiagBbtnClicked(ButtonType btnType)
    {
        switch (btnType)
        {
            case ButtonType.Skip:
                StopCoroutine(_displayNextButtonWithDelay);
                _displayNextButtonWithDelay = null;
                DisplayMain();
                break;
            case ButtonType.Next:
                DisplayMain();
                break;
            case ButtonType.Shop:
                DisplayShop();
                break;
            case ButtonType.Talk:
                DisplayTalk();
                break;
            case ButtonType.GetQuest:
                EventService.Instance.GetQuesDiagBtnClicked?.Invoke();
                break;
            case ButtonType.Exit:
                EventService.Instance.HeroLeaving?.Invoke();
                break;
            case ButtonType.AnsA:
                DisplayMain();
                break;
            case ButtonType.AnsB:
                DisplayMain();
                break;
            case ButtonType.Back:
                break;
        }
    }

    private IEnumerator InvokeWithDelay(Action action, float sec)
    {
        yield return new WaitForSeconds(sec);
        action?.Invoke();
    }
}

public class BehaviorPatterns
{

}
