using UnityEngine;

public class DeliveryProcessor : MonoBehaviour
{
    public DeliveryState State = new DeliveryState();

    public void Init(int totalOrders, int baseRewardSum)
    {
        State.TotalOrders = totalOrders;
        State.DeliveredOrders = 0;
        State.BaseRewardSum = baseRewardSum;
        State.Satisfaction = 1f;
        State.OnTimeRate = 1f;
        State.RouteEfficiency = 1f;
    }

    public void MarkDelivered(bool onTime)
    {
        State.DeliveredOrders++;
        if (!onTime)
        {
            State.OnTimeRate = Mathf.Clamp01(State.OnTimeRate - 0.1f);
            State.Satisfaction = Mathf.Clamp01(State.Satisfaction - 0.05f);
        }
    }

    public bool IsFailed()
    {
        return State.DeliveredOrders < State.TotalOrders;
    }

    public float SpeedMultiplier = 1f;

    public void ApplyEventPenalty(float timePenalty, float satisfactionPenalty)
    {
        State.Satisfaction = Mathf.Clamp01(State.Satisfaction - satisfactionPenalty);
        State.RouteEfficiency = Mathf.Clamp01(State.RouteEfficiency - timePenalty);
        SpeedMultiplier = Mathf.Clamp(SpeedMultiplier - timePenalty, 0.6f, 1f);
    }
}
