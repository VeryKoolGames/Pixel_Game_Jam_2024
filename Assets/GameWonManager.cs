using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

[Serializable]
public class FishStatUI
{
    public TextMeshProUGUI fishType;
    public TextMeshProUGUI fishCount;
    public RectTransform animateUI;
}

public class GameWonManager : MonoBehaviour
{
    private Dictionary<FishTypes, int> _fishCount = new Dictionary<FishTypes, int>();
    [SerializeField] private FishStatUI[] fishStatUIs;
    // Start is called before the first frame update
    void Start()
    {
        _fishCount = FishCreator.Instance.GetFishCount();
        SetUI();
    }

    // Update is called once per frame
    private void SetUI()
    {
        int i = 0;
        foreach (var fishCount in _fishCount)
        {
            fishStatUIs[i].fishCount.text = fishCount.Value.ToString();
            fishStatUIs[i].fishType.text = fishCount.Key.ToString();
        }
    }
}
