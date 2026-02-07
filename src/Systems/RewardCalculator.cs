using UnityEngine;

public class RewardCalculator
{
    public static int CalculateCoins(float score, int baseReward)
    {
        float multiplier = 0.8f + score; // score in [0,1] => multiplier [0.8,1.8]
        return Mathf.RoundToInt(baseReward * multiplier);
    }
}
