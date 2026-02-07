using UnityEngine;

public class ScoreCalculator
{
    public static float Calculate(float onTimeRate, float efficiency, float satisfaction)
    {
        return (onTimeRate * 0.4f) + (efficiency * 0.3f) + (satisfaction * 0.3f);
    }
}
