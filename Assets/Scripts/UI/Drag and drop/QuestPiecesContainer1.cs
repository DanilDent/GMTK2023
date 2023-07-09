using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestPiecesContainer1 : MonoBehaviour
{
    [SerializeField] private List<QuestInformation> _questPieces;

    // Start is called before the first frame update
    private void Start()
    {
        ////int index = 0;
        //get all children, and initialize them

        //foreach (var piece in GetChildren())
        //{
        //    piece.Initialize(this, index);
        //    index++;
        //}

        //_questPieces = GetChildren();

        EventService.Instance.QuestLifetimeEnded += DeleteQuestInformation;
        EventService.Instance.NewQuestBecomeAvailable += AddQuestInformation;
    }

    private void OnDestroy()
    {
        EventService.Instance.QuestLifetimeEnded -= DeleteQuestInformation;
        EventService.Instance.NewQuestBecomeAvailable -= AddQuestInformation;
    }

    public void AddQuestInformation(Quest quest)
    {
        var panel = _questPieces.Where(panel => !panel.gameObject.activeSelf).First();
        panel.Initialize(quest);
        panel.GetComponent<TooltipComponent>().Text = quest.Description;
        panel.gameObject.SetActive(true);
    }

    public void DeleteQuestInformation(Quest quest)
    {
        var panel = _questPieces.Where(panel => panel.Quest.Name == quest.Name).First();
        panel.gameObject.SetActive(false);
    }
}