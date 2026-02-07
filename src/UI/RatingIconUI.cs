using UnityEngine;
using UnityEngine.UI;

public class RatingIconUI : MonoBehaviour
{
    public Image Icon;
    public Sprite S;
    public Sprite A;
    public Sprite B;
    public Sprite C;

    public void Bind(string rating)
    {
        if (Icon == null) return;
        if (rating == "S") Icon.sprite = S;
        else if (rating == "A") Icon.sprite = A;
        else if (rating == "B") Icon.sprite = B;
        else Icon.sprite = C;
    }
}
