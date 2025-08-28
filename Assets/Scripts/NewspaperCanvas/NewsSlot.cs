using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewsSlot : MonoBehaviour, IDropHandler
{
    [Header("Slot Data")] public Vector2Int gridPosition; // Grid'deki pozisyonu (0,0) (1,2) vs
    public Vector2Int slotSize; // Kaç cell kapladığı (1,1) (2,1) vs

    [Header("References")] public GridManager gridManager; // Ana grid manager referansı
    public RectTransform rectTransform; // Pozisyon/boyut için

    [Header("Visual Components")] public Image backgroundImage; // Slot background
    public Text headlineText; // Haber başlığı
    public Image newsImage; // Haber görseli
    public GameObject[] resizeHandles; // 8 adet resize handle

    public void Initialize(Vector2Int gridPos, Vector2Int size)
    {
    }


    public void OnDrop(PointerEventData eventData)
    {
    }
    //public void SetNews(NewsData newsData);
    //public void StartResize(ResizeDirection direction);
    //private void UpdateVisuals();
    //public void ClearSlot();
}