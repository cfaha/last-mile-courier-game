using UnityEngine;
using UnityEngine.UI;

public class OrderStatusUI : MonoBehaviour
{
    public Image StatusIcon;

    public void SetStatus(DeliveryStatus status)
    {
        if (StatusIcon == null) return;
        StatusIcon.color = status == DeliveryStatus.Delivered ? Color.green : Color.gray;
    }
}
