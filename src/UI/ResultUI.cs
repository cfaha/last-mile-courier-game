using UnityEngine;

public class ResultUI : MonoBehaviour
{
    public Text ScoreText;
    public Text CoinText;
    public Text RatingText;
    public Text HintText;
    public RatingIconUI RatingIconUI;
    public UnityEngine.UI.Button NextButton;

    public FlowController FlowController;

    public void ShowResult(float score, int coins, bool failed)
    {
        string rating = RatingHelper.GetRating(score);
        Debug.Log($"Result score={score:F2} rating={rating} coins={coins}");
        if (ScoreText != null) ScoreText.text = $"{score:F2}";
        if (CoinText != null) CoinText.text = $"+{coins}";
        if (RatingText != null) RatingText.text = rating;
        RatingIconUI?.Bind(rating);
        BindButtons(HintText);
        if (NextButton != null) NextButton.interactable = !failed;
    }

    public void OnNextLevel()
    {
        FlowController?.NextLevel();
        FindObjectOfType<MainMenuUI>()?.BindProgress(FlowController != null ? FlowController.CurrentLevelId : 0);
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
