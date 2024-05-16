using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using KBCore.Refs;
using UnityEngine;

[RequireComponent(typeof(OnFishSpawnListener), typeof(AchievmentUI))]
public class FishdexHandler : ValidatedMonoBehaviour
{
    private List<Fish> fishdex = new List<Fish>();
    [SerializeField, Self] private OnFishSpawnListener onFishSpawnListener;
    [SerializeField, Self] private AchievmentUI achievmentUI;
    // Start is called before the first frame update
    void Awake()
    {
        onFishSpawnListener.Response.AddListener(AddFish);
    }

    public void AddFish(GameObject fishObj)
    {
        Debug.Log("Adding fish to fishdex");
        Fish fish = fishObj.GetComponent<FishReproductionManager>().fish;
        if (!fishdex.Contains(fish))
        {
            Debug.Log("Added " + fish + " to fishdex");
            fishdex.Add(fish);
            achievmentUI.OnAchievmentUnlocked(fish);
        }
    }
}
