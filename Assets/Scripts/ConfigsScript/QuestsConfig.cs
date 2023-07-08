using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Configs/QuestConfig", order = 1)]
public class QuestsConfig : ScriptableObject
{
    [SerializeField] private Quest[] data;
    public Quest[] Data { get => data; }
}
