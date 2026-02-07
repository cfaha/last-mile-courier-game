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
    public MainMenuUI MainMenuUI;
    public MapNodeLabel[] MapLabels;
    public Color PendingColor = Color.gray;
    public Color DeliveredColor = Color.white;
    public OrderListController OrderListController;

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
            MainMenuUI?.BindTask(TaskSystem.DailyCompleted, TaskSystem.DailyDeliveriesTarget);
        }
        HighlightNode(orderId.Value);
        OrderListController?.MarkDelivered(orderId.Value);
        OnDelivered?.Invoke(orderId.Value);
        if (Sequence != null && Sequence.Remaining <= 0)
        {
            OnAllDelivered?.Invoke();
        }
    }

    private void HighlightNode(int orderId)
    {
        if (MapLabels == null) return;
        int idx = Mathf.Clamp(orderId - 1, 0, MapLabels.Length - 1);
        var label = MapLabels[idx];
        if (label != null && label.Dot != null)
        {
            label.Dot.color = DeliveredColor;
        }
    }

    public void ResetNodeColors()
    {
        if (MapLabels == null) return;
        foreach (var label in MapLabels)
        {
            if (label != null && label.Dot != null) label.Dot.color = PendingColor;
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
