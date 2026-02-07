using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public Text TaskText;

    public void Bind(int completed, int target)
    {
        if (TaskText != null) TaskText.text = $"每日任务：完成 {completed}/{target} 单";
    }

    private void Start()
    {
        var flow = FindObjectOfType<FlowController>();
        if (flow != null && flow.TaskSystem != null)
        {
            Bind(flow.TaskSystem.DailyCompleted, flow.TaskSystem.DailyDeliveriesTarget);
        }
    }
}
