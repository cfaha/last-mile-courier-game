using UnityEngine;

public class MapNodeLayout : MonoBehaviour
{
    public Transform[] Nodes;
    public Vector2 Center = new Vector2(0, 0);
    public float Radius = 5f;

    public void ArrangeInCircle()
    {
        if (Nodes == null || Nodes.Length == 0) return;
        float step = 360f / Nodes.Length;
        for (int i = 0; i < Nodes.Length; i++)
        {
            float rad = Mathf.Deg2Rad * (step * i);
            float x = Center.x + Mathf.Cos(rad) * Radius;
            float y = Center.y + Mathf.Sin(rad) * Radius;
            Nodes[i].localPosition = new Vector3(x, y, 0);
        }
    }
}
