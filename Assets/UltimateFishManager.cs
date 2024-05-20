using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UltimateFishManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private string dialog = "You made life in here a litte bit better!";

    [SerializeField] private GameObject canvas;

    [SerializeField] private GameObject endingCanvas;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector2.zero;
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1, 1f));
        sequence.AppendInterval(2f);
        sequence.Append(dialogText.DOFade(0, 1f).OnComplete((() => dialogText.text = dialog)));
        sequence.Append(dialogText.DOFade(1, 1f));
        sequence.AppendInterval(2f);
        sequence.Append(canvas.transform.DOScale(0, 1f)).OnComplete((() => canvas.SetActive(false)));
        sequence.OnComplete(() => endingCanvas.SetActive(true));
    }
}
