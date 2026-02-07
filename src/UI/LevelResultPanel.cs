using UnityEngine;
using UnityEngine.UI;

public class LevelResultPanel : MonoBehaviour
{
    public Text SummaryText;
    public Text DetailText;
    public Text TimeText;
    public Text LevelText;
    public Text OnTimeText;

    public void Bind(int delivered, int total, float score, int levelId = 0)
    {
        if (SummaryText != null)
        {
            SummaryText.text = $"完成 {delivered}/{total} 评分 {score:F2}";
        }
        if (LevelText != null && levelId > 0) LevelText.text = $"关卡 {levelId}";
    }

    public void BindDetail(float onTime, float efficiency)
    {
        if (DetailText != null)
        {
            DetailText.text = $"准时率 {onTime:P0} / 效率 {efficiency:P0}";
        }
        if (OnTimeText != null)
        {
            OnTimeText.text = onTime >= 0.99f ? "全程准时" : "有超时";
        }
    }

    public void BindTime(int seconds)
    {
        if (TimeText != null) TimeText.text = $"耗时 {seconds}s";
    }
}
