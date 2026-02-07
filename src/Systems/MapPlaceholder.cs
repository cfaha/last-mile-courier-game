using UnityEngine;

public class MapPlaceholder : MonoBehaviour
{
    public Transform[] Nodes;
    public LineRenderer Line;
    public MapRouteAnimator Animator;

    public void DrawRoute(int[] orderIds)
    {
        if (Line == null || Nodes == null) return;
        int count = Mathf.Min(orderIds.Length, Nodes.Length);
        Line.positionCount = count;
        for (int i = 0; i < count; i++)
        {
            int idx = Mathf.Clamp(orderIds[i] - 1, 0, Nodes.Length - 1);
            Line.SetPosition(i, Nodes[idx].position);
        }
        Animator?.Play();
    }
}
