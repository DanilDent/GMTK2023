using UnityEngine;

[CreateAssetMenu(fileName = "HelloPhrasesConfig", menuName = "Configs/HelloPhrasesConfig", order = 1)]
public class HelloPhrasesConfig : ScriptableObject
{
    [SerializeField] private string[] phrases;
    public string[] Data => phrases;
}
