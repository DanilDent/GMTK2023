using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[CreateAssetMenu(fileName = "Quests", menuName = "Configs/QuestsConfig", order = 1)]
public class HelloPhrasesConfig
{
    [SerializeField] private string[] phrases;
    public string[] HelloPhrases => phrases;
}
