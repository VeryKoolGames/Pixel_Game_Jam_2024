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

    [SerializeField] private GameObject[] clickCreditsLeave;
    [SerializeField] private GameObject[] clickCreditsEnter;
    private void Start()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.waterAmbiance, Vector3.zero);
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.mainMusic, Vector3.zero);
        MainMenuFishCreator.Instance.OnStartSceneStart();
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

    public void OnCreditsClick()
    {
        var sequence = DOTween.Sequence();
        float delay = 0f;

        foreach (var elem in clickCreditsLeave)
        {
            Vector3 pos = elem.GetComponent<RectTransform>().position;
            pos.x += 60;
            Image thisButton = elem.GetComponent<Image>();
            thisButton.raycastTarget = false;

            sequence.Insert(delay, elem.GetComponent<RectTransform>().DOMove(pos, 0.2f));
            sequence.Insert(delay, elem.GetComponent<Image>().DOFade(0, 0.2f));

            delay += 0.1f;
        }

        foreach (var elem in clickCreditsEnter)
        {
            elem.SetActive(true);
            Vector3 targetPos = elem.GetComponent<RectTransform>().position;
            Vector3 startPos = elem.GetComponent<RectTransform>().position;
            startPos.x -= 60;
            elem.GetComponent<RectTransform>().position = startPos;
            Image thisButton = elem.GetComponent<Image>();
            thisButton.raycastTarget = true;

            sequence.Insert(delay, elem.GetComponent<RectTransform>().DOMove(targetPos, 0.2f));
            sequence.Insert(delay, elem.GetComponent<Image>().DOFade(1, 0.2f));

            delay += 0.1f;

            startPos.x -= 60;
            elem.GetComponent<RectTransform>().position = startPos;
        }

    }

    public void OnBackArrowClick()
    {
        var sequence = DOTween.Sequence();
        float delay = 0f;

        foreach (var elem in clickCreditsEnter)
        {
            elem.SetActive(true);
            Vector3 targetPos = elem.GetComponent<RectTransform>().position;
            targetPos.x += 60;
            Image thisButton = elem.GetComponent<Image>();
            thisButton.raycastTarget = false;

            sequence.Insert(delay, elem.GetComponent<RectTransform>().DOMove(targetPos, 0.2f));
            sequence.Insert(delay, elem.GetComponent<Image>().DOFade(0, 0.2f));

            delay += 0.1f;
        }

        foreach (var elem in clickCreditsLeave)
        {
            elem.SetActive(true);
            Vector3 startPos = elem.GetComponent<ButtonEvents>().targetRectTransform.position;
            elem.GetComponent<RectTransform>().position = startPos;
            Image thisButton = elem.GetComponent<Image>();  
            thisButton.raycastTarget = true;
            
            Vector3 targetPos = elem.GetComponent<ButtonEvents>().startRectTransform.position;

            sequence.Insert(delay, elem.GetComponent<RectTransform>().DOMove(targetPos, 0.2f));
            sequence.Insert(delay, elem.GetComponent<Image>().DOFade(1, 0.2f));

            delay += 0.1f;
             
        }
    }


}
