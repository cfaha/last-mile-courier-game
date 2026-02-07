using UnityEngine;
using UnityEngine.EventSystems;

public class MapNodeHighlighter : MonoBehaviour, IPointerClickHandler
{
    public GameObject Highlight;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Highlight != null) Highlight.SetActive(!Highlight.activeSelf);
    }
}
