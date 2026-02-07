using System;
using UnityEngine;

[Serializable]
public class LevelConfig
{
    public int id;
    public int orders;
    public int time;
    public float eventChance;
}

[Serializable]
public class LevelConfigList
{
    public LevelConfig[] levels;
}

public class LevelConfigLoader
{
    public static LevelConfig GetLevel(TextAsset json, int id)
    {
        var list = JsonUtility.FromJson<LevelConfigList>(json.text);
        foreach (var lvl in list.levels)
        {
            if (lvl.id == id) return lvl;
        }
        return null;
    }
}
