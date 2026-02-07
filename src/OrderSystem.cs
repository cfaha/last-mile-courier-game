using System.Collections.Generic;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{
    public List<OrderData> ActiveOrders = new List<OrderData>();

    public void GenerateOrders(int count)
    {
        ActiveOrders.Clear();
        for (int i = 0; i < count; i++)
        {
            ActiveOrders.Add(new OrderData
            {
                OrderId = i + 1,
                BaseReward = 100,
                TimeLimitSeconds = 300
            });
        }
    }
}
