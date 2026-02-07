using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public FlowController FlowController;
    public UnityEngine.UI.Text ProgressText;

    public void BindProgress(int level)
    {
        if (ProgressText != null) ProgressText.text = $"当前进度：{level}";
    }

    public void OnStart()
    {
        FlowController?.StartPlanning();
    }
}
