using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableNewsItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas _parentCanvas;
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    public Vector2 finalDropPosition;

    public void Awake()
    {
        _parentCanvas = GetComponentInParent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _parentCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        finalDropPosition = eventData.position;
        Debug.Log("Final Drop Position: " + finalDropPosition);
        _canvasGroup.blocksRaycasts = true;
    }


    public void MarkAsDropped()
    {
    }
}