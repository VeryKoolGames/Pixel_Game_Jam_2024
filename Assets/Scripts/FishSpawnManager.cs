using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class FishSpawnManager : MonoBehaviour
{
    [SerializeField] private OnFishSpawn onFishSpawn;
    public void SpawnFish()
    {
        Fish fish = CreateFish();
        onFishSpawn.Raise(fish);
    }
    
    private Fish CreateFish()
    {
        Fish fish = new Fish(FishTypes.TUNA, "Connard de ton", 0, "Tuna fish");
        return fish;
    }
}
