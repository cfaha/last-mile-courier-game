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
            var type = PickOrderType();
            timeLimit -= GetTypeTimePenalty(type);
            int reward = 100 + (int)(distance * 30f) + GetTypeBonus(type);

            ActiveOrders.Add(new OrderData
            {
                OrderId = i + 1,
                BaseReward = reward,
                TimeLimitSeconds = timeLimit,
                Type = type
            });
            RuntimeOrders[i] = new OrderRuntime
            {
                OrderId = i + 1,
                DistanceKm = distance,
                TimeLimitSeconds = timeLimit
            };
        }
    }

    private OrderType PickOrderType()
    {
        float r = Random.value;
        if (r < 0.6f) return OrderType.Normal;
        if (r < 0.75f) return OrderType.Fresh;
        if (r < 0.87f) return OrderType.Insured;
        if (r < 0.95f) return OrderType.Large;
        return OrderType.Night;
    }

    private int GetTypeBonus(OrderType type)
    {
        switch (type)
        {
            case OrderType.Fresh: return 20;
            case OrderType.Insured: return 30;
            case OrderType.Large: return 40;
            case OrderType.Night: return 25;
            default: return 0;
        }
    }

    private int GetTypeTimePenalty(OrderType type)
    {
        switch (type)
        {
            case OrderType.Fresh: return 30;
            case OrderType.Night: return 20;
            case OrderType.Large: return 15;
            default: return 0;
        }
    }
}
