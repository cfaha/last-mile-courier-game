using UnityEngine;

public class MapPlaceholder : MonoBehaviour
{
    public Transform[] Nodes;

    public void DrawRoute(int[] orderIds)
    {
        // TODO: line renderer / path visualize
        Debug.Log($"DrawRoute: {orderIds.Length} nodes");
    }
}
