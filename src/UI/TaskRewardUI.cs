using UnityEngine;
using UnityEngine.UI;

public class TaskRewardUI : MonoBehaviour
{
    public Text Text;

    public void Show(string msg)
    {
        if (Text != null) Text.text = msg;
        gameObject.SetActive(true);
        CancelInvoke(nameof(Hide));
        Invoke(nameof(Hide), 2f);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
