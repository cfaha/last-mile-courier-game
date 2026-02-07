using UnityEngine;
using UnityEngine.UI;

public class OrderItemUI : MonoBehaviour
{
    public Text TitleText;
    public Text MetaText;
    public Button UpButton;
    public Button DownButton;

    public void Bind(OrderData data, float distanceKm)
    {
        if (TitleText != null) TitleText.text = $"订单 #{data.OrderId}";
        if (MetaText != null) MetaText.text = $"距离 {distanceKm:F1}km / 时限 {data.TimeLimitSeconds}s";
    }

    public void SetMoveHandlers(System.Action onUp, System.Action onDown)
    {
        if (UpButton != null)
        {
            UpButton.onClick.RemoveAllListeners();
            UpButton.onClick.AddListener(() => onUp?.Invoke());
        }
        if (DownButton != null)
        {
            DownButton.onClick.RemoveAllListeners();
            DownButton.onClick.AddListener(() => onDown?.Invoke());
        }
    }
}
