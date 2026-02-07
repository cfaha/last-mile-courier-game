using System.Collections.Generic;
using UnityEngine;

public class DeliverySequence : MonoBehaviour
{
    public int CurrentIndex;
    public List<int> OrderIds = new List<int>();

    public int Remaining => OrderIds.Count - CurrentIndex;

    public void SetSequence(List<int> orderIds)
    {
        OrderIds = new List<int>(orderIds);
        CurrentIndex = 0;
    }

    public int? NextOrder()
    {
        if (CurrentIndex >= OrderIds.Count) return null;
        return OrderIds[CurrentIndex++];
    }
}
