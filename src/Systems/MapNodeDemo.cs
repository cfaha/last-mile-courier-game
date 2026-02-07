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
            Labels[i]?.Bind($"{i + 1}");
        }
    }
}
