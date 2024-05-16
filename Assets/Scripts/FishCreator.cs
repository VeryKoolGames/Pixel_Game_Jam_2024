using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class FishCreator : MonoBehaviour
    {
        public static FishCreator Instance { get; private set; }
        [SerializeField] private List<FishSO> fishList = new List<FishSO>();
        [SerializeField] private OnFishSpawn onFishSpawn;
        [SerializeField] private OnFishSpawn onFishSpawnOtherFlock;
        private Dictionary<int, List<FishSO>> fishDictionary = new Dictionary<int, List<FishSO>>();
        [SerializeField] private Transform spawnPoint;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            foreach (FishSO fish in fishList)
            {
                if (fishDictionary.ContainsKey(fish.rarity))
                {
                    fishDictionary[fish.rarity].Add(fish);
                }
                else
                {
                    fishDictionary.Add(fish.rarity, new List<FishSO> {fish});
                }
            }
        }

        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                CreateFish(0);
            }
        }

        public void CreateFish(Fish fishOne, Fish fishTwo)
        {
            int rarity = GetFishRarity(fishOne.FishRarety, fishTwo.FishRarety);
            FishSO fishSO = GetFishSO(rarity);
            Debug.Log(fishSO + " of rarity: " + rarity);
            SpawnFish(fishSO);
        }
        
        public void CreateFish(FishSO fishSO)
        {
            SpawnFish(fishSO);
        }
        
        private void SpawnFish(FishSO fishSO)
        {
            if (fishSO != null)
            {
                Fish fish = new Fish(fishSO.fishType, fishSO.name, fishSO.rarity, fishSO.description, fishSO.sprite);
                GameObject obj = Instantiate(fishSO.prefab, spawnPoint.position, Quaternion.identity);
                obj.GetComponent<FishReproductionManager>().SetFish(fish);
                GetRandomEvent().Raise(obj);
            }
        }
        
        private OnFishSpawn GetRandomEvent()
        {
            if (Random.Range(0f, 1f) < 0.5f)
            {
                return onFishSpawnOtherFlock;
            }
            return onFishSpawn;
        }
        
        public void CreateFish(int rarity)
        {
            FishSO fishSO = GetFishSO(rarity);
            SpawnFish(fishSO);
        }
        
        private FishSO GetFishSO(int rarity)
        {
            if (fishDictionary.ContainsKey(rarity))
            {
                return fishDictionary[rarity][Random.Range(0, fishDictionary[rarity].Count)];
            }
            return null;
        }
        
        private int GetFishRarity(int rarityOne, int rarityTwo)
        {
            if (rarityOne == rarityTwo)
            {
                if (Random.Range(0f, 1f) < 0.8f)
                {
                    return rarityOne;
                }
                if (rarityOne + 1 > fishDictionary.Count)
                {
                    return rarityOne;
                }
                return rarityOne + 1;
            }
            return Random.Range(rarityOne, rarityTwo);
        }
        
    }
}