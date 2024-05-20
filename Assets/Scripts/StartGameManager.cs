using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FMOD.Studio;
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
        // EventInstance mainMusic = AudioManager.Instance.CreateInstance(FmodEvents.Instance.waterAmbiance);
        // EventInstance amianceSounds = AudioManager.Instance.CreateInstance(FmodEvents.Instance.mainMusic);
        // mainMusic.start();
        // amianceSounds.start();
    }

    public void OnGameStart()
    {
        fadeImage.transform.gameObject.SetActive(true);
        canvas.DOScaleX(0, 1f);
        fadeImage.DOFade(0, 1f).OnComplete((() => gameObject.SetActive(false)));
    }
}
