using UnityEngine;

public class MapNodeDemo : MonoBehaviour
{
    public MapNodeLayout Layout;

    private void Start()
    {
        Layout?.ArrangeInCircle();
    }
}
