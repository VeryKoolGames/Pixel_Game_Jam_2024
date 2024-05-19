using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasCardHandler : ValidatedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField, Self] private RectTransform rectTransform;
    [SerializeField] private RectTransform targetTransform;
    private Vector3 baseTransform;
    private Vector3 baseScale;
    private Vector3 targetScale;
    private bool isPointerOver = false; // Flag to track pointer state
    private bool isZoomed = false; // Flag to track zoom state

    // Start is called before the first frame update
    void Start()
    {
        baseTransform = rectTransform.transform.position;
        baseScale = rectTransform.transform.localScale;
        targetScale = targetTransform.transform.localScale;
        targetScale.x = 9;
        targetScale.y = 9;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPointerOver && !isZoomed)
        {
            Debug.Log("Pointer Enter");
            isPointerOver = true;
            rectTransform.DOMoveY(baseTransform.y - 40f, .2f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPointerOver && !isZoomed)
        {
            isPointerOver = false;
            rectTransform.DOMove(baseTransform, .2f);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isZoomed = true;
        rectTransform.DOMove(targetTransform.transform.position, .7f);
        rectTransform.DOScale(targetScale, .7f);
    }
}