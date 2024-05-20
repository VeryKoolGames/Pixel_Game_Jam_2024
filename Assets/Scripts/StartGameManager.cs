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
    [SerializeField] private RectTransform targetTransform;
    [SerializeField] private Image fadeImage;
    private void Start()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        canvas.DOMove(targetTransform.position, 2f).SetEase(Ease.InOutQuad).OnComplete((() =>
        {
            FishCreator.Instance.OnGameSceneStart();
            gameObject.SetActive(false);
        }));;
    }
}
