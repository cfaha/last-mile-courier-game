using UnityEngine;

public partial class FlowController
{
    private void ApplyEvent(float timePenalty, float satisfactionPenalty)
    {
        DeliveryProcessor?.ApplyEventPenalty(timePenalty, satisfactionPenalty);
        if (DeliverySimulator != null && DeliveryProcessor != null)
        {
            DeliverySimulator.SpeedMultiplier = DeliveryProcessor.SpeedMultiplier;
        }
    }
}
