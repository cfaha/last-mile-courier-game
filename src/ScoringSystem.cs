using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    public float OnTimeRate;
    public float RouteEfficiency;
    public float Satisfaction;

    public float CalculateScore()
    {
        return (OnTimeRate * 0.4f) + (RouteEfficiency * 0.3f) + (Satisfaction * 0.3f);
    }
}
