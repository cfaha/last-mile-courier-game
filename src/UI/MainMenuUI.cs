using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public FlowController FlowController;
    public UnityEngine.UI.Text ProgressText;
    public TaskUI TaskUI;
    public CurrencyUI CurrencyUI;

    public void BindProgress(int level)
    {
        if (ProgressText != null) ProgressText.text = $"当前进度：{level}";
    }

    public void BindTask(int completed, int target)
    {
        TaskUI?.Bind(completed, target);
    }

    public void BindCoins(int coins)
    {
        CurrencyUI?.Bind(coins);
    }

    public void OnStart()
    {
        FlowController?.StartPlanning();
    }

    public void OnContinue()
    {
        FlowController?.StartPlanning();
    }
}
