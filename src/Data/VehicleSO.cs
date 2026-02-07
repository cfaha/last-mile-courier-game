using UnityEngine;

[CreateAssetMenu(menuName = "LastMile/Vehicle")]
public class VehicleSO : ScriptableObject
{
    public string VehicleName;
    public float SpeedMultiplier;
    public int Durability;
}
