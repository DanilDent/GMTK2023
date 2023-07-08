using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Image _heroAvatarImg;
    [SerializeField] private TextMeshProUGUI _heroNicknameText;

    private HeroManager _heroManager;
    private EventService _eventService;

    private void Start()
    {
        _heroManager = HeroManager.Instance;
        _eventService = EventService.Instance;
        //
        _eventService.NewHeroComing += HandleNewHeroComing;
    }

    private void HandleNewHeroComing(string heroNickname)
    {
        Hero hero = _heroManager.Heroes.FirstOrDefault(_ => _.Nickname == heroNickname);
        _heroAvatarImg.sprite = hero.CurrentAvatarParts[0].Value;
        _heroNicknameText.text = hero.Nickname;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _eventService.NewHeroComing -= HandleNewHeroComing;
    }
}