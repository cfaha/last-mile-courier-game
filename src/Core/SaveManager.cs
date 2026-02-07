using UnityEngine;

public class SaveManager
{
    private const string LevelKey = "lm_level";

    public static void SaveLevel(int level)
    {
        PlayerPrefs.SetInt(LevelKey, level);
        PlayerPrefs.Save();
    }

    public static int LoadLevel(int defaultLevel = 1)
    {
        return PlayerPrefs.GetInt(LevelKey, defaultLevel);
    }
}
