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
    [SerializeField] private RectTransform target;
    [SerializeField] private Image fadeImage; 
    private void Start()
    {
        for (int i = 0; i < NumberOfFish; i++)
        {
            FishCreator.Instance.CreateFish(0);
            FishCreator.Instance.RandomizeSpawnPoint();
        }
    }

    public void OnGameStart()
    {
        fadeImage.transform.gameObject.SetActive(true);
        canvas.DOMove(target.position, 1f)
        //fadeImage.DOFade(1, 1f)
          .OnComplete((() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1)));
    }
}
