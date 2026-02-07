using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public Text TaskText;

    public void Bind(int completed, int target)
    {
        if (TaskText != null) TaskText.text = $"每日任务：完成 {completed}/{target} 单";
    }
}
