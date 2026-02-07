using System.Collections.Generic;
using UnityEngine;

public class RouteDragController : MonoBehaviour
{
    public List<int> CurrentOrderIds = new List<int>();

    public void SetOrders(List<int> orderIds)
    {
        CurrentOrderIds = new List<int>(orderIds);
    }

    public void Swap(int indexA, int indexB)
    {
        if (indexA < 0 || indexB < 0 || indexA >= CurrentOrderIds.Count || indexB >= CurrentOrderIds.Count) return;
        int temp = CurrentOrderIds[indexA];
        CurrentOrderIds[indexA] = CurrentOrderIds[indexB];
        CurrentOrderIds[indexB] = temp;
    }
}
