using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeTests : MonoBehaviour
{
    private GameTime time1 = new(0, new Vector2Int(0, 0));
    private GameTime time2 = new(0, new Vector2Int(3, 3));
    private GameTime time3 = new(0, new Vector2Int(1, 40));
    private GameTime time4 = new(1, new Vector2Int(1, 7));
    private GameTime time5 = new(1, new Vector2Int(0, 21));
    private GameTime time6 = new(1, new Vector2Int(0, 21));
    private void Start()
    {
        Debug.Log($"1) {time1}");

        Debug.Log($"2) {time1 + time2} | Res: 0:3:3");
        Debug.Log($"3) {time4 + time2} | Res: 1:4:10");
        Debug.Log($"4) {time3 + time5} | Res: 1:2:1");

        Debug.Log($"5) {time3 - time4} | Res: 0:0:0");
        Debug.Log($"6) {time2 - time3} | Res: 0:1:23");
        Debug.Log($"7) {time3 - time2} | Res: 0:0:0");
        Debug.Log($"8) {time4 - time2} | Res: 0:22:4");

        Debug.Log($"9) {time5 == time6} | Res: true");
        Debug.Log($"10) {time5 == time4} | Res: false");
        Debug.Log($"11) {time5 != time4} | Res: true");
        Debug.Log($"12) {time5 != time4} | Res: true");

        Debug.Log($"13) {time5 < time6} | Res: false");
        Debug.Log($"14) {time5 <= time6} | Res: true");
        Debug.Log($"13) {time5 > time6} | Res: false");
        Debug.Log($"14) {time5 >= time6} | Res: true");

        Debug.Log($"15) {time1 < time2} | Res: true");
        Debug.Log($"16) {time1 <= time2} | Res: true");
        Debug.Log($"17) {time1 > time2} | Res: false");
        Debug.Log($"18) {time1 >= time2} | Res: false");
    }
}
