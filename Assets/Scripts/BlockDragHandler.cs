using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDragHandler : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Grid grid;
    private Vector3 originalPosition;
    private Camera mainCamera;
    private BlockSpawnScaler blockScaler;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
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
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;
        transform.position = GetMouseWorldPosition() + offset;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        transform.position = grid.GetCellCenterWorld(cellPosition);
        if (blockScaler != null)
        {
            blockScaler.OnMouseUp();
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}