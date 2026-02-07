using System.Collections.Generic;
using UnityEngine;

public class RoutePlanningUI : MonoBehaviour
{
    public RouteDragController DragController;
    public OrderListController ListController;

    public void BindOrders(List<OrderData> orders, OrderRuntime[] runtime)
    {
        var ids = new List<int>();
        foreach (var order in orders)
        {
            ids.Add(order.OrderId);
            Debug.Log($"Order #{order.OrderId} reward={order.BaseReward} time={order.TimeLimitSeconds}s");
        }
        DragController?.SetOrders(ids);
        ListController?.Build(orders, runtime);
    }

    public FlowController FlowController;

    public void OnStartDelivery()
    {
        FlowController?.StartDelivery();
    }
}
