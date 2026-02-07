using UnityEngine;
using UnityEngine.UI;

public class LevelResultPanel : MonoBehaviour
{
    public Text SummaryText;

    public void Bind(int delivered, int total, float score)
    {
        if (SummaryText != null)
        {
            SummaryText.text = $"完成 {delivered}/{total} 评分 {score:F2}";
        }
    }
}
