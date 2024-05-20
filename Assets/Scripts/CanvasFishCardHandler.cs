using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasFishCardHandler : ValidatedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform newPopUp;
    [SerializeField] RectTransform cardTransform;
    [SerializeField, Self] private OnCardCanvasZoomListener onCardCanvasZoom;
    private Vector3 baseTransform;
    private bool isPointerOver = false; // Flag to track pointer state
    private Tween tween;

    private bool isZoomed;
    // Start is called before the first frame update
    void Start()
    {
        onCardCanvasZoom.Response.AddListener(SetIsZoomed);
    }

    private void SetIsZoomed(bool isZoomed)
    {
        this.isZoomed = isZoomed;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPointerOver && isZoomed)
        {
            isPointerOver = false;
            cardTransform.DOMove(baseTransform, .2f);
            tween?.Kill();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPointerOver && isZoomed)
        {
            newPopUp.gameObject.SetActive(false);
            baseTransform = cardTransform.transform.position;
            isPointerOver = true;
            tween = cardTransform.DOMoveY(baseTransform.y - 10f, 1f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.bubblePop, transform.position);
        }
    }
}
