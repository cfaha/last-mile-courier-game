using UnityEngine;
using UnityEngine.UI;

public class MapNodeLabel : MonoBehaviour
{
    public Text Label;

    public void Bind(string text)
    {
        if (Label != null) Label.text = text;
    }
}
