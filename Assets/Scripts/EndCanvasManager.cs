using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;


[Serializable]
public class FishDataUI
{
    public FishTypes fishType;
    public TextMeshProUGUI fishNumber;
}

public class EndCanvasManager : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;
    // Start is called before the first frame update
    Dictionary<FishTypes, int> fishStats = new Dictionary<FishTypes, int>();
    [SerializeField] private List<FishDataUI> fishDataUI;

    void Start()
    {
        canvas.localScale = Vector2.zero;
        fishStats = FishCreator.Instance.GetEndingData();
        SetDataOnCanvas();
        canvas.DOScale(1, 1f);
    }

    public void closeCanvas()
    {
        canvas.DOScale(0, .4f).OnComplete((() => canvas.gameObject.SetActive(false)));
    }
    
    private void SetDataOnCanvas()
    {
        foreach (var x in fishStats)
        {
            foreach (var y in fishDataUI)
            {
                if (x.Key == y.fishType)
                {
                    y.fishNumber.text = x.Value.ToString();
                }
            }
        }
    }
}
