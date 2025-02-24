using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockSpawnScaler : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 targetScale = Vector3.one;
    

    private void Start()
    {
        
        originalScale = transform.localScale;
        targetScale = originalScale * 1.2f;
        transform.localScale = originalScale * 0.8f; 
    }


    public void OnMouseDown()
    {
        
        transform.DOScale(targetScale, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnMouseUp()
    {
        
        transform.DOScale(originalScale * 0.8f, 0.2f).SetEase(Ease.InBack);
    }
}
