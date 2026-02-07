using UnityEngine;
using UnityEngine.UI;

public class ToastUI : MonoBehaviour
{
    public Text Text;
    public ToastAnimator Animator;

    public void Show(string msg)
    {
        if (Text != null) Text.text = msg;
        gameObject.SetActive(true);
        Animator?.Play();
        CancelInvoke(nameof(Hide));
        Invoke(nameof(Hide), 2f);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
