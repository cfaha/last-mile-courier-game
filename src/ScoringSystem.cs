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
}

