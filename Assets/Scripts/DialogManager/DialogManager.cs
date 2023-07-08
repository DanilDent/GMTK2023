using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private HelloPhrasesConfig helloPhrasesConfig;
    [SerializeField] private DialogConfig dialogConfig;


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
}

public class BehaviorPatterns
{

}
