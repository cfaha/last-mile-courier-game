using UnityEngine;
using UnityEngine.UI;

public class LevelResultPanel : MonoBehaviour
{
    public Text SummaryText;
    public Text DetailText;
    public Text TimeText;
    public Text LevelText;
    public Text OnTimeText;
    public Text TotalCoinText;

    public void Bind(int delivered, int total, float score, int levelId = 0, int totalCoins = -1)
    {
        if (SummaryText != null)
        {
            float rate = total > 0 ? (float)delivered / total : 0f;
            SummaryText.text = $"完成 {delivered}/{total} 评分 {score:F2} 达成率 {rate:P0}";
        }
        if (LevelText != null && levelId > 0) LevelText.text = $"关卡 {levelId}";
        if (TotalCoinText != null && totalCoins >= 0) TotalCoinText.text = $"金币 {totalCoins}";
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
