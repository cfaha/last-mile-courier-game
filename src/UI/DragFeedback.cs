using UnityEngine;
using UnityEngine.EventSystems;

public class DragFeedback : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Highlight;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Highlight != null) Highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Highlight != null) Highlight.SetActive(false);
    }
}
