using UnityEngine;
using UnityEngine.UI;

public class CompletionUI : MonoBehaviour
{
    public Text TitleText;

    public void Show()
    {
        if (TitleText != null) TitleText.text = "已通关！";
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
