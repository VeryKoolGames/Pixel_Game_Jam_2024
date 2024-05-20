using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Image fadeImage; 
    private void Start()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        fadeImage.transform.gameObject.SetActive(true);
        canvas.DOMoveX(-100, 1f);
        fadeImage.DOFade(0, 1f).OnComplete((() =>
        {
            FishCreator.Instance.OnGameSceneStart();
            gameObject.SetActive(false);
        }));
    }
}
