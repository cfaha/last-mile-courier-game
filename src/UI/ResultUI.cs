using UnityEngine;

public class ResultUI : MonoBehaviour
{
    public Text ScoreText;
    public Text CoinText;
    public Text RatingText;
    public Text HintText;
    public RatingIconUI RatingIconUI;
    public UnityEngine.UI.Button NextButton;
    public Text FailText;
    public Text StateText;
    public UnityEngine.UI.Button ReplayButton;

    public FlowController FlowController;

    public void ShowResult(float score, int coins, bool failed)
    {
        string rating = RatingHelper.GetRating(score);
        Debug.Log($"Result score={score:F2} rating={rating} coins={coins}");
        if (ScoreText != null) ScoreText.text = $"{score:F2}";
        if (CoinText != null) CoinText.text = $"+{coins}";
        if (RatingText != null) RatingText.text = rating;
        RatingIconUI?.Bind(rating);
        BindButtons(HintText, failed);
        if (NextButton != null) NextButton.interactable = !failed;
        if (ReplayButton != null) ReplayButton.interactable = true;
        HighlightButtons(failed);
        if (FailText != null) FailText.text = failed ? "失败：未完成全部订单" : "";
        if (StateText != null) StateText.text = failed ? "失败" : "成功";
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

    public void BindButtons(UnityEngine.UI.Text hint, bool failed)
    {
        if (hint != null && string.IsNullOrEmpty(hint.text))
        {
            hint.text = failed ? "失败时可重玩" : "可进入下一关";
        }
    }

    private void HighlightButtons(bool failed)
    {
        if (NextButton != null) NextButton.transform.localScale = failed ? Vector3.one * 0.9f : Vector3.one * 1.05f;
        if (ReplayButton != null) ReplayButton.transform.localScale = failed ? Vector3.one * 1.05f : Vector3.one * 0.95f;
    }

    public void HideHint(UnityEngine.UI.Text hint)
    {
        if (hint != null) hint.text = "";
    }
}
