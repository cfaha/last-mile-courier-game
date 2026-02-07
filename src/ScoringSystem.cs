using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    public float OnTimeRate;
    public float RouteEfficiency;
    public float Satisfaction;

    public float CalculateScore()
    {
        return ScoreCalculator.Calculate(OnTimeRate, RouteEfficiency, Satisfaction);
    }

    public void SyncFromState(DeliveryState state)
    {
        OnTimeRate = state.OnTimeRate;
        RouteEfficiency = state.RouteEfficiency;
        Satisfaction = state.Satisfaction;
    }
}

