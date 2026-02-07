using UnityEngine;
using UnityEngine.UI;

public class TutorialOverlay : MonoBehaviour
{
    public Text Text;

    public void Show(string msg)
    {
        if (Text != null) Text.text = msg;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
