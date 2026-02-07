using UnityEngine;

public class MapNodeDemo : MonoBehaviour
{
    public MapNodeLayout Layout;
    public MapNodeLabel[] Labels;

    private void Start()
    {
        Layout?.ArrangeInCircle();
        for (int i = 0; i < Labels.Length; i++)
        {
            var type = (OrderType)(i % 5);
            Labels[i]?.Bind($"{i + 1}", type);
        }
    }
}
