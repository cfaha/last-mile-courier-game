using UnityEngine;

public class DeliverySimulator : MonoBehaviour
{
    public DeliverySequence Sequence;
    public DeliveryProcessor Processor;

    public float SpeedMultiplier = 1f;
    public OrderRuntime[] Orders;

    public System.Action<int> OnDelivered;
    public System.Action OnAllDelivered;
    public TaskSystem TaskSystem;
    public TaskUI TaskUI;

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

        TaskSystem?.OnDeliveryComplete();
        if (TaskSystem != null && TaskUI != null)
        {
            TaskUI.Bind(TaskSystem.DailyCompleted, TaskSystem.DailyDeliveriesTarget);
        }
        OnDelivered?.Invoke(orderId.Value);
        if (Sequence != null && Sequence.Remaining <= 0)
        {
            OnAllDelivered?.Invoke();
        }
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
