using System.Collections.Generic;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{
    public List<OrderData> ActiveOrders = new List<OrderData>();

    public OrderRuntime[] RuntimeOrders;

    public void GenerateOrders(int count, int overrideTimeLimit = -1)
    {
        ActiveOrders.Clear();
        RuntimeOrders = new OrderRuntime[count];
        for (int i = 0; i < count; i++)
        {
            int timeLimit = overrideTimeLimit > 0 ? overrideTimeLimit : Random.Range(180, 420);
            float distance = Random.Range(0.5f, 3.0f);
            ActiveOrders.Add(new OrderData
            {
                OrderId = i + 1,
                BaseReward = 100 + (int)(distance * 30f),
                TimeLimitSeconds = timeLimit
            });
            RuntimeOrders[i] = new OrderRuntime
            {
                OrderId = i + 1,
                DistanceKm = distance,
                TimeLimitSeconds = timeLimit
            };
        }
    }
}
