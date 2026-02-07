using System;
using UnityEngine;

[Serializable]
public class ShopItemList
{
    public ShopItem[] items;
}

public class ShopConfig
{
    public static ShopItem[] Load(TextAsset json)
    {
        var list = JsonUtility.FromJson<ShopItemList>(json.text);
        return list.items;
    }
}
