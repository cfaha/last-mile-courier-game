using UnityEngine;

public class SaveManager
{
    private const string LevelKey = "lm_level";
    private const string CoinKey = "lm_coins";
    private const string TaskKey = "lm_tasks";
    private const string OwnedKey = "lm_owned";

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

    public static void SaveTaskProgress(int completed, int dayStamp, bool claimed)
    {
        PlayerPrefs.SetString(TaskKey, $"{completed}|{dayStamp}|{(claimed ? 1 : 0)}");
        PlayerPrefs.Save();
    }

    public static void LoadTaskProgress(out int completed, out int dayStamp, out bool claimed)
    {
        completed = 0; dayStamp = 0; claimed = false;
        var raw = PlayerPrefs.GetString(TaskKey, "");
        if (string.IsNullOrEmpty(raw)) return;
        var parts = raw.Split('|');
        if (parts.Length >= 3)
        {
            int.TryParse(parts[0], out completed);
            int.TryParse(parts[1], out dayStamp);
            claimed = parts[2] == "1";
        }
    }

    public static void SaveOwned(string csv)
    {
        PlayerPrefs.SetString(OwnedKey, csv);
        PlayerPrefs.Save();
    }

    public static string LoadOwned()
    {
        return PlayerPrefs.GetString(OwnedKey, "");
    }
}
