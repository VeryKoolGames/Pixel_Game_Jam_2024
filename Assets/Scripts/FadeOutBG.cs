using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeOutBG : MonoBehaviour
{
    public RectTransform target;
    public RectTransform canvas;
    void Start()
    {
        
        canvas.DOMove(target.position, 1f).SetEase(Ease.InCubic);
    }

}
