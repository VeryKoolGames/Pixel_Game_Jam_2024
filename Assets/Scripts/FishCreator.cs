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
                FishCustomization fishAttributes = GetFishAttributes(fishSO);
                GameObject obj = Instantiate(fishSO.prefab, spawnPoint.position, Quaternion.identity);
                // obj.SetActive(false);
                obj.GetComponent<FishCustomizeManager>().CustomizeFish(fishAttributes);
                obj.GetComponent<FishReproductionManager>().SetFish(fish);
                GetRandomEvent().Raise(obj);
            }
        }

        private FishCustomization GetFishAttributes(FishSO fishSo)
        {
            Sprite eyeSprite = fishSo.eyes[Random.Range(0, fishSo.eyes.Length)];
            Sprite topFinSprite = fishSo.topFins[Random.Range(0, fishSo.topFins.Length)];
            Sprite bodyPatternSprite = fishSo.bodyPatterns[Random.Range(0, fishSo.bodyPatterns.Length)];
            Sprite tailSprite = fishSo.tails[Random.Range(0, fishSo.tails.Length)];
            Color bodyColor = fishSo.bodyColors[Random.Range(0, fishSo.bodyColors.Length)];
            Color otherColor = fishSo.otherColors[Random.Range(0, fishSo.otherColors.Length)];
            Color patternColor = fishSo.patternColors[Random.Range(0, fishSo.patternColors.Length)];
            return new FishCustomization(bodyPatternSprite, tailSprite, topFinSprite, eyeSprite, bodyColor, otherColor, patternColor);
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
            Debug.Log(fishSO + " of rarity: " + rarity);
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

        public void RandomizeSpawnPoint()
        {
            spawnPoint.position = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }
    }
}