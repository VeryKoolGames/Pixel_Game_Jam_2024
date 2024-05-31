using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KBCore.Refs;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasCardHandler : ValidatedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField, Self] private RectTransform rectTransform;
    [SerializeField] private RectTransform targetTransform;
    [SerializeField] private Sprite outlinedIndex;
    [SerializeField] private Sprite Index;
    private Vector3 baseTransform;
    private Vector3 baseScale;
    private Vector3 targetScale;
    private bool isPointerOver = false; // Flag to track pointer state
    private bool isZoomed = false; // Flag to track zoom state
    [SerializeField] private OnCardCanvasZoom onCardCanvasZoom;

    // Start is called before the first frame update
    void Start()
    {
        baseTransform = rectTransform.transform.position;
        baseScale = rectTransform.transform.localScale;
        targetScale = targetTransform.transform.localScale;
        targetScale.x = 9;
        targetScale.y = 9;
    }
    
    public void CloseZoom()
    {
        isZoomed = false;
        rectTransform.DOMove(baseTransform, .7f);
        // rectTransform.DOScale(baseScale, .7f).OnComplete(() => onCardCanvasZoom.Raise(false));
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Index;
    }
    
    void Update()
    {
        if (isZoomed && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(rectTransform))
            {
                CloseZoom();
            }
        }
    }

    private bool IsPointerOverUIObject(RectTransform rectTransform)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject == rectTransform.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPointerOver && !isZoomed)
        {
            isPointerOver = true;
            rectTransform.DOMoveY(baseTransform.y - 80f, .2f);
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = outlinedIndex;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPointerOver && !isZoomed)
        {
            isPointerOver = false;
            rectTransform.DOMove(baseTransform, .2f);
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Index;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isZoomed = true;
        rectTransform.DOMove(targetTransform.transform.position, .7f);
        // rectTransform.DOScale(targetScale, .7f).OnComplete(() => onCardCanvasZoom.Raise(true));
    }
    
    public void PlaySoundOnButtonClick()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.chestSound, transform.position);
    }
}