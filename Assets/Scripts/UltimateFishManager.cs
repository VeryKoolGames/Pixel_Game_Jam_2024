using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UltimateFishManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private string dialog = "You made life in here a litte bit better!";
    [SerializeField] private Transform location;
    [SerializeField] private SpriteRenderer headSprite;
    [SerializeField] private Image fishIndexSprite;
    [SerializeField] private Sprite fishIndexSpriteUpdated;
    [SerializeField] private Sprite bothHeadSprite;
    [SerializeField] private GameObject buttonIndexCanvas;

    [SerializeField] private GameObject canvas;

    [SerializeField] private GameObject endingCanvas;
    Tweener moveTween;
    // Start is called before the first frame update
    void Start()
    {
        moveTween = transform.DOLocalMoveY(.001f, .5f).SetLoops(-1, LoopType.Yoyo);
        ReadDialogue();
    }
    
    private void MoveUpAndDown()
    {
        transform.DOLocalMoveY(.2f, .5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void ReadDialogue()
    {
        transform.localScale = Vector2.zero;
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1, 1f));
        sequence.AppendInterval(1f);
        sequence.Append(dialogText.DOFade(0, 1f).OnComplete((() => dialogText.text = dialog)));
        sequence.Append(dialogText.DOFade(1, 1f));
        sequence.Append(canvas.transform.DOScale(0, 1f)).OnComplete((() =>
        {
            canvas.SetActive(false);
            MoveTowardsHouse();
        }));
    }

    private void MoveTowardsHouse()
    {
        moveTween.Kill();
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(location.position, 1f));
        sequence.AppendInterval(.2f);
        sequence.Append(transform.DOScale(0, 1f));
        sequence.Append(headSprite.DOFade(0, 1f).OnComplete((() => headSprite.sprite = bothHeadSprite)));
        sequence.Append(headSprite.DOFade(1, 1f));
        sequence.AppendInterval(2f);
        sequence.OnComplete(() =>
        {
            endingCanvas.SetActive(true);
            fishIndexSprite.sprite = fishIndexSpriteUpdated;
            buttonIndexCanvas.SetActive(true);
        });
    }
}
