using System.Collections.Generic;
using UnityEngine;

public class RoutePlanningUI : MonoBehaviour
{
    public RouteDragController DragController;
    public Transform ListRoot;
    public OrderItemUI ItemPrefab;

    public void BindOrders(List<OrderData> orders)
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
                item.Bind(order, 1.0f + i * 0.2f);
            }
        }
        DragController?.SetOrders(ids);
    }

    public void OnStartDelivery()
    {
        // TODO: start delivery flow
    }
}
