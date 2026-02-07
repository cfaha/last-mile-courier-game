using UnityEngine;

[CreateAssetMenu(menuName = "LastMile/Order")]
public class OrderSO : ScriptableObject
{
    public string OrderName;
    public int BaseReward;
    public int TimeLimitSeconds;
}
