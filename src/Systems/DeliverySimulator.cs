using UnityEngine;

public class DeliverySimulator : MonoBehaviour
{
    public DeliverySequence Sequence;
    public DeliveryProcessor Processor;

    public float SpeedMultiplier = 1f;
    public OrderRuntime[] Orders;

    public void DeliverNext()
    {
        int? orderId = Sequence?.NextOrder();
        if (orderId == null) return;
        var order = FindOrder(orderId.Value);
        if (order == null) return;

        int travelSeconds = TravelTimeEstimator.EstimateSeconds(order.DistanceKm, SpeedMultiplier);
        bool onTime = travelSeconds <= order.TimeLimitSeconds;
        Processor?.MarkDelivered(onTime);
        Debug.Log($"Delivered order #{orderId} travel={travelSeconds}s onTime={onTime} speed={SpeedMultiplier:F2}");
    }

    private OrderRuntime FindOrder(int orderId)
    {
        if (Orders == null) return null;
        foreach (var o in Orders)
        {
            if (o.OrderId == orderId) return o;
        }
        return null;
    }
}
