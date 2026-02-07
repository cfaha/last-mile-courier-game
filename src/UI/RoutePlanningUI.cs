using System.Collections.Generic;
using UnityEngine;

public class RoutePlanningUI : MonoBehaviour
{
    public RouteDragController DragController;
    public Transform ListRoot;
    public OrderItemUI ItemPrefab;

    public void BindOrders(List<OrderData> orders, OrderRuntime[] runtime)
    {
        // TODO: render draggable list
        var ids = new List<int>();
        for (int i = 0; i < orders.Count; i++)
        {
            var order = orders[i];
            ids.Add(order.OrderId);
            Debug.Log($"Order #{order.OrderId} reward={order.BaseReward} time={order.TimeLimitSeconds}s");

            if (ItemPrefab != null && ListRoot != null)
            {
                var item = Object.Instantiate(ItemPrefab, ListRoot);
                float dist = (runtime != null && i < runtime.Length) ? runtime[i].DistanceKm : (1.0f + i * 0.2f);
                item.Bind(order, dist);
            }
        }
        DragController?.SetOrders(ids);
    }

    public FlowController FlowController;

    public void OnStartDelivery()
    {
        FlowController?.StartDelivery();
    }
}
