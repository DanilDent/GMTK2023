using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Configs/QuestConfig", order = 1)]
public class QuestConfig : ScriptableObject
{
    [SerializeField] private Quest[] data;
    public Quest[] GetData { get => data; }
}
