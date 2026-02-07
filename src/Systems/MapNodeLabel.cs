using UnityEngine;
using UnityEngine.UI;

public class MapNodeLabel : MonoBehaviour
{
    public Text Label;
    public UnityEngine.UI.Image Dot;

    public void Bind(string text, OrderType type = OrderType.Normal)
    {
        if (Label != null) Label.text = text;
        if (Dot != null) Dot.color = OrderTypeColor.Get(type);
    }
}
