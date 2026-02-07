using UnityEngine;

public class TravelTimeEstimator
{
    public static int EstimateSeconds(float distanceKm, float speedMultiplier)
    {
        // base speed 20 km/h => 1 km = 180s
        float baseSeconds = distanceKm * 180f;
        return Mathf.RoundToInt(baseSeconds / Mathf.Max(0.1f, speedMultiplier));
    }
}
