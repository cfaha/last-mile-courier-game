using UnityEngine;

public class SaveManager
{
    private const string LevelKey = "lm_level";
    private const string CoinKey = "lm_coins";

    public static void SaveLevel(int level)
    {
        PlayerPrefs.SetInt(LevelKey, level);
        PlayerPrefs.Save();
    }

    public static int LoadLevel(int defaultLevel = 1)
    {
        return PlayerPrefs.GetInt(LevelKey, defaultLevel);
    }

    public static void SaveCoins(int coins)
    {
        PlayerPrefs.SetInt(CoinKey, coins);
        PlayerPrefs.Save();
    }

    public static int LoadCoins(int defaultCoins = 0)
    {
        return PlayerPrefs.GetInt(CoinKey, defaultCoins);
    }
}
