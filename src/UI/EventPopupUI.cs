using UnityEngine;
using UnityEngine.UI;

public class EventPopupUI : MonoBehaviour
{
    public Text TitleText;
    public Text DescText;
    public Button WaitButton;
    public Button DetourButton;

    public Text WaitText;
    public Text DetourText;

    public void Show(string title, string desc, System.Action onWait, System.Action onDetour)
    {
        if (TitleText != null) TitleText.text = title;
        if (DescText != null) DescText.text = desc;
        if (WaitText != null) WaitText.text = "等待处理（较慢）";
        if (DetourText != null) DetourText.text = "绕行处理（较轻惩罚）";
        if (WaitButton != null)
        {
            WaitButton.onClick.RemoveAllListeners();
            WaitButton.onClick.AddListener(() => { onWait?.Invoke(); Hide(); });
        }
        if (DetourButton != null)
        {
            DetourButton.onClick.RemoveAllListeners();
            DetourButton.onClick.AddListener(() => { onDetour?.Invoke(); Hide(); });
        }
        gameObject.SetActive(true);
        CancelInvoke(nameof(Hide));
        Invoke(nameof(Hide), 5f);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
