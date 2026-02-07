using UnityEngine;

public class ResultUI : MonoBehaviour
{
    public Text ScoreText;
    public Text CoinText;
    public Text RatingText;
    public Text HintText;

    public FlowController FlowController;

    public void ShowResult(float score, int coins)
    {
        string rating = RatingHelper.GetRating(score);
        Debug.Log($"Result score={score:F2} rating={rating} coins={coins}");
        if (ScoreText != null) ScoreText.text = $"{score:F2}";
        if (CoinText != null) CoinText.text = $"+{coins}";
        if (RatingText != null) RatingText.text = rating;
        BindButtons(HintText);
    }

    public void OnNextLevel()
    {
        FlowController?.NextLevel();
    }

    public void OnReplay()
    {
        FlowController?.StartPlanning();
    }

    public void BindButtons(UnityEngine.UI.Text hint)
    {
        if (hint != null && string.IsNullOrEmpty(hint.text))
        {
            hint.text = "可重玩或进入下一关";
        }
    }

    public void HideHint(UnityEngine.UI.Text hint)
    {
        if (hint != null) hint.text = "";
    }
}
