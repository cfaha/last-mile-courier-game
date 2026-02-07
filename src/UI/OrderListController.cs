using System.Collections.Generic;
using UnityEngine;

public class OrderListController : MonoBehaviour
{
    public RouteDragController DragController;
    public Transform ListRoot;
    public OrderItemUI ItemPrefab;
    private readonly List<OrderItemUI> _items = new List<OrderItemUI>();

    public void Build(List<OrderData> orders, OrderRuntime[] runtime)
    {
        Clear();
        for (int i = 0; i < orders.Count; i++)
        {
            var order = orders[i];
            float dist = (runtime != null && i < runtime.Length) ? runtime[i].DistanceKm : (1.0f + i * 0.2f);
            var item = Object.Instantiate(ItemPrefab, ListRoot);
            item.Bind(order, dist);
            int index = i;
            item.SetMoveHandlers(
                () => Move(index, -1),
                () => Move(index, 1)
            );
            _items.Add(item);
        }
    }

    private void Move(int index, int delta)
    {
        int target = index + delta;
        if (target < 0 || target >= _items.Count) return;
        DragController?.Swap(index, target);
        // swap items in UI list
        var tmp = _items[index];
        _items[index] = _items[target];
        _items[target] = tmp;
        // update hierarchy order
        _items[index].transform.SetSiblingIndex(index);
        _items[target].transform.SetSiblingIndex(target);
    }

    private void Clear()
    {
        foreach (var item in _items)
        {
            if (item != null) Destroy(item.gameObject);
        }
        _items.Clear();
    }
}
