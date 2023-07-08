using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        helloPhrases = helloPhrasesConfig.Data;
        allDurationMessage = dialogConfig.AllDurationMessage;
    }

    void Update()
    {

    }

    public void UpdateDialogueText(string text)
    {
        _dialogueText.text = text;
    }

    public void DisplayHello()
    {
        UpdateDialogueText(helloPhrases[Random.Range(0, helloPhrases.Length)]);
        Display(0, 1);
    }

    public void DisplayMain()
    {
        Display(2, 3, 4, 5);
    }

    public void DisplayShop()
    {
        Display(5);
    }

    public void DisplayTalk()
    {
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
}

public class BehaviorPatterns
{

}
