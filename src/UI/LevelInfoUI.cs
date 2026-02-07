using UnityEngine;
using UnityEngine.UI;

public class LevelInfoUI : MonoBehaviour
{
    public Text LevelText;
    public Text ZoneText;

    public void Bind(int levelId, ZoneType zone)
    {
        if (LevelText != null) LevelText.text = $"关卡 {levelId}";
        if (ZoneText != null) ZoneText.text = zone.ToString();
    }
}
