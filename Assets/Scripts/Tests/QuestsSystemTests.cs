/*
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class QuestsSystemTests : MonoBehaviour
{
    GameManager gameManager;
    QuestManager questManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance; 
        questManager = QuestManager.Instance;
        gameManager.IsPaused = true;
        questManager.invisibleQuests.Clear();
        questManager.avalaibleQuests.Clear();
        questManager.inProgressQuests.Clear();

        Quest[] testQuests = new Quest[]
        {
            new Quest(
            new GameTime(0, new Vector2Int(0,0)),
            new GameTime(2, new Vector2Int(0,0)),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            true),
            new Quest(
            new GameTime(1, new Vector2Int(0,0)),
            new GameTime(1, new Vector2Int(12,0)),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            true),
            new Quest(
            new GameTime(1, new Vector2Int(0,0)),
            new GameTime(1, new Vector2Int(0,0)),
            new News("", new GameTime(1, new Vector2Int(0,0))),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            true),
            new Quest(
            new GameTime(2, new Vector2Int(0,0)),
            new GameTime(1, new Vector2Int(0,0)),
            new News("", new GameTime(1, new Vector2Int(0,0))),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            false),
            new Quest(
            new GameTime(10, new Vector2Int(0,0)),
            new GameTime(2, new Vector2Int(0,0)),
            new News("", new GameTime(1, new Vector2Int(0,0))),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            false),
            new Quest(
            new GameTime(10, new Vector2Int(0,0)),
            new GameTime(2, new Vector2Int(0,0)),
            new News("", new GameTime(1, new Vector2Int(0,0))),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            false),
            new Quest(
            new GameTime(10, new Vector2Int(0,0)),
            new GameTime(2, new Vector2Int(0,0)),
            new News("", new GameTime(1, new Vector2Int(0,0))),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            false),
            new Quest(
            new GameTime(10, new Vector2Int(0,0)),
            new GameTime(2, new Vector2Int(0,0)),
            new News("", new GameTime(1, new Vector2Int(0,0))),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            false),
            new Quest(
            new GameTime(10, new Vector2Int(0,0)),
            new GameTime(2, new Vector2Int(0,0)),
            new News("", new GameTime(1, new Vector2Int(0,0))),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            false),
            new Quest(
            new GameTime(10, new Vector2Int(0,0)),
            new GameTime(2, new Vector2Int(0,0)),
            new News("", new GameTime(1, new Vector2Int(0,0))),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            false),
            new Quest(
            new GameTime(10, new Vector2Int(0,0)),
            new GameTime(2, new Vector2Int(0,0)),
            new News("", new GameTime(1, new Vector2Int(0,0))),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            false),
            new Quest(
            new GameTime(10, new Vector2Int(0,0)),
            new GameTime(2, new Vector2Int(0,0)),
            new News("", new GameTime(1, new Vector2Int(0,0))),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            false),
            new Quest(
            new GameTime(10, new Vector2Int(0,0)),
            new GameTime(2, new Vector2Int(0,0)),
            new News("", new GameTime(1, new Vector2Int(0,0))),
            new News("", new GameTime(0, new Vector2Int(12,0))),
            false),


        }; 
        questManager.invisibleQuests.AddRange(testQuests);
        Test1();
        Test2();
        Test3();
        Test4();
        Test5();
        Test6();
        Test7();
    }

    private void Test1()
    {
        gameManager.SetCurrentTime(new GameTime(0, new Vector2Int(0, 0)));
        questManager.OnGameTimeUpdate();
        var invQuestsCount = questManager.invisibleQuests.Count;
        var avalQuestsCount = questManager.avalaibleQuests.Count;
        var progQuestCount = questManager.inProgressQuests.Count;

        var correctRes = new Vector3Int(12, 1, 0);
        var result = invQuestsCount == correctRes.x && avalQuestsCount == correctRes.y && progQuestCount == correctRes.z;
        Debug.Log($"Test 1) IQ={invQuestsCount} AQ={avalQuestsCount} PQ={progQuestCount} | {correctRes} | {result}");
    }

    private void Test2()
    {
        gameManager.SetCurrentTime(new GameTime(1, new Vector2Int(0, 0)));
        questManager.OnGameTimeUpdate();
        var invQuestsCount = questManager.invisibleQuests.Count;
        var avalQuestsCount = questManager.avalaibleQuests.Count;
        var progQuestCount = questManager.inProgressQuests.Count;

        var correctRes = new Vector3Int(10, 3, 0);
        var result = invQuestsCount == correctRes.x && avalQuestsCount == correctRes.y && progQuestCount == correctRes.z;
        Debug.Log($"Test 2) IQ={invQuestsCount} AQ={avalQuestsCount} PQ={progQuestCount} | {correctRes} | {result}");
    }

    private void Test3()
    {
        gameManager.SetCurrentTime(new GameTime(1, new Vector2Int(2, 0)));
        questManager.OnQuestAssigned(questManager.avalaibleQuests[0]);
        questManager.OnGameTimeUpdate();
        var invQuestsCount = questManager.invisibleQuests.Count;
        var avalQuestsCount = questManager.avalaibleQuests.Count;
        var progQuestCount = questManager.inProgressQuests.Count;
        
        var correctRes = new Vector3Int(10, 2, 1);
        var result = invQuestsCount == correctRes.x && avalQuestsCount == correctRes.y && progQuestCount == correctRes.z;
        Debug.Log($"Test 3) IQ={invQuestsCount} AQ={avalQuestsCount} PQ={progQuestCount} | {correctRes} | {result}");
    }
    private void Test4()
    {
        gameManager.SetCurrentTime(new GameTime(1, new Vector2Int(20, 0)));
        questManager.OnGameTimeUpdate();
        var invQuestsCount = questManager.invisibleQuests.Count;
        var avalQuestsCount = questManager.avalaibleQuests.Count;
        var progQuestCount = questManager.inProgressQuests.Count;

        var correctRes = new Vector3Int(10, 2, 0);
        var result = invQuestsCount == correctRes.x && avalQuestsCount == correctRes.y && progQuestCount == correctRes.z;
        Debug.Log($"Test 4) IQ={invQuestsCount} AQ={avalQuestsCount} PQ={progQuestCount} | {correctRes} | {result}");
    }

    private void Test5()
    {
        gameManager.SetCurrentTime(new GameTime(2, new Vector2Int(1, 0)));
        questManager.OnGameTimeUpdate();
        var invQuestsCount = questManager.invisibleQuests.Count;
        var avalQuestsCount = questManager.avalaibleQuests.Count;
        var progQuestCount = questManager.inProgressQuests.Count;

        var correctRes = new Vector3Int(9, 1, 0);
        var result = invQuestsCount == correctRes.x && avalQuestsCount == correctRes.y && progQuestCount == correctRes.z;
        Debug.Log($"Test 5) IQ={invQuestsCount} AQ={avalQuestsCount} PQ={progQuestCount} | {correctRes} | {result}");
    }
    private void Test6()
    {
        gameManager.SetCurrentTime(new GameTime(5, new Vector2Int(1, 0)));
        questManager.OnGameTimeUpdate();
        gameManager.SetCurrentTime(new GameTime(10, new Vector2Int(1, 0)));
        questManager.OnGameTimeUpdate();
        var invQuestsCount = questManager.invisibleQuests.Count;
        var avalQuestsCount = questManager.avalaibleQuests.Count;
        var progQuestCount = questManager.inProgressQuests.Count;

        var correctRes = new Vector3Int(3, 6, 0);
        var result = invQuestsCount == correctRes.x && avalQuestsCount == correctRes.y && progQuestCount == correctRes.z;
        Debug.Log($"Test 6) IQ={invQuestsCount} AQ={avalQuestsCount} PQ={progQuestCount} | {correctRes} | {result}");
    }

    private void Test7()
    {
        gameManager.SetCurrentTime(new GameTime(20, new Vector2Int(1, 0)));
        questManager.OnGameTimeUpdate();
        var invQuestsCount = questManager.invisibleQuests.Count;
        var avalQuestsCount = questManager.avalaibleQuests.Count;
        var progQuestCount = questManager.inProgressQuests.Count;

        var correctRes = new Vector3Int(0, 3, 0);
        var result = invQuestsCount == correctRes.x && avalQuestsCount == correctRes.y && progQuestCount == correctRes.z;
        Debug.Log($"Test 7) IQ={invQuestsCount} AQ={avalQuestsCount} PQ={progQuestCount} | {correctRes} | {result}");
    }
}
*/
