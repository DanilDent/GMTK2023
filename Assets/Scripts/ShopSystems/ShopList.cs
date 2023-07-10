using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopList", menuName = "ShopList", order = 0)]
public class ShopList : ScriptableObject
{
    public int RemainingMoney;
    public List<string> Entries;
    public float AnimationTimeMilliSeconds = 2000f;
    public float TimeToDisappearMilliSeconds = 2000f;
    public static ShopList GetDefault()
    {
        var shopList = CreateInstance<ShopList>();
        shopList.Entries = new List<string>();
        shopList.AnimationTimeMilliSeconds = 2000f;
        shopList.TimeToDisappearMilliSeconds = 2000f;
        return shopList;
    }
}