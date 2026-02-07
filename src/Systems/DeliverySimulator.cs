using UnityEngine;

public class DeliverySimulator : MonoBehaviour
{
    public DeliverySequence Sequence;
    public DeliveryProcessor Processor;

    public void DeliverNext()
    {
        int? orderId = Sequence?.NextOrder();
        if (orderId == null) return;
        // TODO: replace with time-based delivery
        Processor?.MarkDelivered(onTime: true);
        Debug.Log($"Delivered order #{orderId}");
    }
}
