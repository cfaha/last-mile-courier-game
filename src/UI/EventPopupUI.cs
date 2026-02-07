using UnityEngine;
using UnityEngine.UI;

public class EventPopupUI : MonoBehaviour
{
    public Text TitleText;
    public Text DescText;
    public Button WaitButton;
    public Button DetourButton;

    public void Show(string title, string desc, System.Action onWait, System.Action onDetour)
    {
        if (TitleText != null) TitleText.text = title;
        if (DescText != null) DescText.text = desc;
        if (WaitButton != null)
        {
            WaitButton.onClick.RemoveAllListeners();
            WaitButton.onClick.AddListener(() => onWait?.Invoke());
        }
        if (DetourButton != null)
        {
            DetourButton.onClick.RemoveAllListeners();
            DetourButton.onClick.AddListener(() => onDetour?.Invoke());
        }
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
