using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour
{
    [SerializeField] private int NumberOfFish;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Image fadeImage; 
    private void Start()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        fadeImage.transform.gameObject.SetActive(true);
        canvas.DOScaleX(0, 1f);
        fadeImage.DOFade(0, 1f).OnComplete((() => gameObject.SetActive(false)));
    }
}
