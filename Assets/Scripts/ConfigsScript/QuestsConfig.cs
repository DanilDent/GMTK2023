using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Configs/QuestsConfig", order = 1)]
public class QuestsConfig : ScriptableObject
{
    [SerializeField] private Quest[] data;
    public Quest[] GetData { get => data; }
}
