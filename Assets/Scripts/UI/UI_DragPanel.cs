using UnityEngine;
using UnityEngine.EventSystems;

// https://usingsystem.tistory.com/417
public class UI_DragPanel : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    Vector2 _moveBegin;
    Vector2 _startingPoint;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _moveBegin = eventData.position;
        _startingPoint = this.transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 moveOffset = eventData.position - _moveBegin;
        this.transform.position = _startingPoint + moveOffset;
    }
}