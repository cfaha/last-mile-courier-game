using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableOrderItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public int Index;
    public System.Action<int, int> OnSwap;

    private RectTransform _rect;
    private CanvasGroup _group;
    private Vector3 _startPos;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _group = GetComponent<CanvasGroup>();
    }

    public void SetIndex(int index)
    {
        Index = index;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPos = _rect.position;
        if (_group != null)
        {
            _group.blocksRaycasts = false;
            _group.alpha = 0.6f;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _rect.position = _startPos;
        if (_group != null)
        {
            _group.blocksRaycasts = true;
            _group.alpha = 1f;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        var other = eventData.pointerDrag?.GetComponent<DraggableOrderItem>();
        if (other != null && other != this)
        {
            OnSwap?.Invoke(other.Index, Index);
        }
    }
}
