using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDragHandler : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private BlockSpawnScaler blockScaler;

    private void Start()
    {
        mainCamera = Camera.main;
        blockScaler = GetComponent<BlockSpawnScaler>(); 
    }

    private void OnMouseDown()
    {
        
        offset = transform.position - GetMouseWorldPosition();

        
        if (blockScaler != null)
        {
            blockScaler.OnMouseDown();
        }
    }

    private void OnMouseDrag()
    {
        
        transform.position = GetMouseWorldPosition() + offset;
    }

    private void OnMouseUp()
    {
       
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
