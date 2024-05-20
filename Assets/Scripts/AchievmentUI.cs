using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievmentUI : MonoBehaviour
{
    [SerializeField] private RectTransform targetTransform;
    [SerializeField] private RectTransform achievmentTransform;
    [SerializeField] private TextMeshProUGUI textFishName;
    [SerializeField] private Image fishImage;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = achievmentTransform.position;
    }

    public void OnAchievmentUnlocked(Fish fish)
    {
        UpdateUIPosition();
        UpdateUIContent(fish);
    }

    private void UpdateUIContent(Fish fish)
    {
        textFishName.text = fish.FishName;
        fishImage.sprite = fish.FishSprite;
    }

    private void UpdateUIPosition()
    {
        achievmentTransform.DOMove(targetTransform.position, 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            DOVirtual.DelayedCall(3f, () =>
            {
                achievmentTransform.DOMove(_startPosition, 1f).SetEase(Ease.OutQuad);
            });
        });
    }
}
