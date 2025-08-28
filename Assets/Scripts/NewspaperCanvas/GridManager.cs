using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridManager : MonoBehaviour, IDropHandler
{
    [Header("Grid Settings")] public int rows = 12;
    public int columns = 8;
    public int gridWidth = 800;
    public int gridHeight = 1200; 
    public Vector2Int cellSize = new Vector2Int(100, 100);

    [Header("Scroll View ")] public ScrollRect scrollView;
    public RectTransform contentArea;
    
    private Dictionary<Vector2Int, NewsSlot> _occupiedCells = new Dictionary<Vector2Int, NewsSlot>();
    private Vector2 totalGridSize;

    
    void Start()
    {
        // ScrollView content area boyutunu ayarla
        totalGridSize = new Vector2(gridWidth * cellSize.x, gridHeight * cellSize.y);
        contentArea.sizeDelta = totalGridSize;
    
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        throw new System.NotImplementedException();
    }


    public Vector2 GridToWorldPosition(Vector2Int gridPos)
    {
        return new Vector2(gridPos.x * cellSize.x, gridPos.y * cellSize.y);
    }
    public Vector2Int WorldToGridPosition(Vector2 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / cellSize.x);
        int y = Mathf.RoundToInt(worldPos.y / cellSize.y);
        return new Vector2Int(x, y);
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}