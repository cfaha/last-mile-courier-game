using System.Collections.Generic;
using UnityEngine;

public class RoutePlanningUI : MonoBehaviour
{
    public RouteDragController DragController;

    public void BindOrders(List<OrderData> orders)
    {
        // TODO: render draggable list
        var ids = new List<int>();
        foreach (var order in orders)
        {
            ids.Add(order.OrderId);
            Debug.Log($"Order #{order.OrderId} reward={order.BaseReward} time={order.TimeLimitSeconds}");
        }
        DragController?.SetOrders(ids);
    }

    public void OnStartDelivery()
    {
        // TODO: start delivery flow
    }
}
