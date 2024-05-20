using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using KBCore.Refs;
using UnityEngine;

[RequireComponent(typeof(OnFishSpawnListener), typeof(AchievmentUI))]
public class FishdexHandler : ValidatedMonoBehaviour
{
    private List<FishTypes> fishdex = new List<FishTypes>();
    [SerializeField] private OnFishSpawnListener onFishSpawnListener;
    [SerializeField] private OnFishSpawnListener onFishSpawnListener2;
    [SerializeField, Self] private AchievmentUI achievmentUI;
    [SerializeField, Self] private AchievmentBook achievmentBook;
    // Start is called before the first frame update
    void Awake()
    {
        onFishSpawnListener.Response.AddListener(AddFish);
        onFishSpawnListener2.Response.AddListener(AddFish);
    }

    public void AddFish(GameObject fishObj)
    {
        Fish fish = fishObj.GetComponent<FishReproductionManager>().fish;
        if (!fishdex.Contains(fish.FishType))
        {
            Debug.Log("Added " + fish + " to fishdex");
            fishdex.Add(fish.FishType);
            achievmentUI.OnAchievmentUnlocked(fish);
            achievmentBook.OnAchievmentUnlocked(fish);
        }
    }
}
