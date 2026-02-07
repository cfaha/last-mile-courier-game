using System.Collections.Generic;
using UnityEngine;

public class RouteSystem : MonoBehaviour
{
    public List<int> RouteOrderIds = new List<int>();

    public void SetRoute(List<int> orderIds)
    {
        RouteOrderIds = new List<int>(orderIds);
    }
}
