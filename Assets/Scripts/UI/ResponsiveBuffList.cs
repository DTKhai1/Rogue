using UnityEngine;
using UnityEngine.UI;

public class ResponsiveBuffList : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField] private float minCellSize = 80f;
    [SerializeField] private float maxCellSize = 120f;
    [SerializeField] private int targetItemsPerRow = 4;
    [SerializeField] private float spacing = 10f;

    private void Awake()
    {
        // Auto-get the GridLayoutGroup if not assigned
        if (gridLayout == null)
            gridLayout = GetComponent<GridLayoutGroup>();

        // Initial update
        UpdateGridLayout();
    }

    private void OnRectTransformDimensionsChange()
    {
        UpdateGridLayout();
    }

    private void UpdateGridLayout()
    {
        if (gridLayout == null) return;

        // Get the width of our content area
        RectTransform rect = transform as RectTransform;
        if (rect == null) return;

        float availableWidth = rect.rect.width;
        // Calculate cell size based on available width and desired items per row
        float calculatedSize = (availableWidth - (spacing * (targetItemsPerRow - 1))) / targetItemsPerRow;
        float cellSize = Mathf.Clamp(calculatedSize, minCellSize, maxCellSize);

        // Update grid layout settings
        gridLayout.cellSize = new Vector2(cellSize, cellSize);
        gridLayout.spacing = new Vector2(spacing, spacing);
    }
}