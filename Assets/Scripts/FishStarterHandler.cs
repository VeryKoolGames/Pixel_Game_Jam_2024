using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class FishStarterHandler : MonoBehaviour
{
    [SerializeField] private FishSO _fishSo;
    [SerializeField] private GameObject canvas;
    // Start is called before the first frame update
    public void OnFishChoice()
    {
        FishCreator.Instance.CreateFish(_fishSo);
        FishCreator.Instance.CreateFish(_fishSo);
        FishCreator.Instance.CreateFish(_fishSo);
        canvas.SetActive(false);
    }
}
