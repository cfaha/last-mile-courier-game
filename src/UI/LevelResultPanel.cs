using UnityEngine;
using UnityEngine.UI;

public class LevelResultPanel : MonoBehaviour
{
    public Text SummaryText;
    public Text DetailText;

    public void Bind(int delivered, int total, float score)
    {
        if (SummaryText != null)
        {
            SummaryText.text = $"完成 {delivered}/{total} 评分 {score:F2}";
        }
    }

    public void BindDetail(float onTime, float efficiency)
    {
        if (DetailText != null)
        {
            DetailText.text = $"准时率 {onTime:P0} / 效率 {efficiency:P0}";
        }
    }
}
