using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockSpawnScaler : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 targetScale;


    private void Start()
    {

        originalScale = Vector3.one;
        targetScale = originalScale * 1.05f;
        transform.localScale = originalScale * 0.8f;
    }


    public void OnMouseDown()
    {

        transform.DOScale(targetScale, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnMouseUp()
    {

        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InBack);
    }
}