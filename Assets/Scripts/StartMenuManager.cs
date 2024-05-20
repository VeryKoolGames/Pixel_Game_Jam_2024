using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private int NumberOfFish;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private GameObject[] elementsToMove;
    [SerializeField] private RectTransform target;
    [SerializeField] private Image fadeImage; 
    private void Start()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.waterAmbiance, Vector3.zero);
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.mainMusic, Vector3.zero);
        FishCreator.Instance.OnStartSceneStart();
    }

    public void OnGameStart()
    {
        fadeImage.transform.gameObject.SetActive(true);
        var sequence = DOTween.Sequence();
        float delay = 0f;

        foreach (var elem in elementsToMove)
        {
            Vector3 pos = elem.GetComponent<RectTransform>().position;
            pos.x += 60;

            sequence.Insert(delay, elem.GetComponent<RectTransform>().DOMove(pos, 0.2f));
            sequence.Insert(delay, elem.GetComponent<Image>().DOFade(0, 0.2f));

            delay += 0.1f;
        }

        sequence.Insert(delay, canvas.DOMove(target.position, 1f).SetEase(Ease.InCubic));
        sequence.OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }
}
