using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockDragHandler : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Grid grid;
    private Vector3 originalPosition;
    private Camera mainCamera;
    private BlockSpawnScaler blockScaler;
    
    private static Dictionary<Vector3Int, bool> occupiedCells = new Dictionary<Vector3Int, bool>();

    private List<Vector3Int> currentBlockCells = new List<Vector3Int>();
    private Tilemap tilemap;
    private BoundsInt gridBounds;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        tilemap = FindObjectOfType<Tilemap>();

        gridBounds = tilemap.cellBounds;
        originalPosition = transform.position;
        mainCamera = Camera.main;
        blockScaler = GetComponent<BlockSpawnScaler>();
    }

    private void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();


        if (blockScaler != null)
        {
            blockScaler.OnMouseDown();
        }

        foreach (var cell in currentBlockCells)
        {
            if (occupiedCells.ContainsKey(cell))
            {
                occupiedCells.Remove(cell);
            }
        }
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;
        transform.position = GetMouseWorldPosition() + offset;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        List<Vector3Int> newBlockCells = GetOccupiedCells();

        
        bool isValidPosition = true;
        foreach (var cell in newBlockCells)
        {
            if (!IsInsideGridBounds(cell))
            {
                isValidPosition = false;
                break;
            }
            if (occupiedCells.ContainsKey(cell)) 
            {
                isValidPosition = false;
                break;
            }
        }

        if (isValidPosition) 
        {
            
            currentBlockCells = newBlockCells;
            foreach (var cell in currentBlockCells)
            {
                occupiedCells[cell] = true;
            }
            
            SnapToGrid(); 
            originalPosition = transform.position;
        }
        else 
        {
            transform.position = originalPosition;

            
            foreach (var cell in currentBlockCells)
            {
                occupiedCells[cell] = true;
            }
        }

        if (blockScaler != null)
        {
            blockScaler.OnMouseUp();
        }
    }
    private List<Vector3Int> GetOccupiedCells()
    {
        List<Vector3Int> cells = new List<Vector3Int>();

        
        foreach (Transform child in transform)
        {
            Vector3Int cellPosition = grid.WorldToCell(child.position);
            if (!cells.Contains(cellPosition))
            {
                cells.Add(cellPosition);
            }
        }

        return cells;
    }
    private void SnapToGrid()
    {
        
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        transform.position = grid.GetCellCenterWorld(cellPosition);
    }
    private bool IsInsideGridBounds(Vector3Int cell)
    {
        return gridBounds.Contains(cell);
    }
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}