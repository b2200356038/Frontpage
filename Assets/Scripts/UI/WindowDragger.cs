using UnityEngine;
using UnityEngine.EventSystems;

public class WindowDragger : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform windowRectTransform;
    private Canvas parentCanvas;
    private Camera canvasCamera;
    private Vector2 dragOffset;
    
    void Start()
    {
        windowRectTransform = transform.parent.GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>();
        canvasCamera = parentCanvas.worldCamera ?? Camera.main;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 currentPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)parentCanvas.transform,
            eventData.position,
            canvasCamera,
            out currentPointerPosition);
    
        // Vector3'ü Vector2'ye cast et
        dragOffset = (Vector2)windowRectTransform.localPosition - currentPointerPosition;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            eventData.position,
            canvasCamera,
            out localPointerPosition);
            
        // Offset'i uygula
        windowRectTransform.localPosition = localPointerPosition + dragOffset;
    }
}