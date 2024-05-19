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
        [SerializeField] private Flock flockOne;
        [SerializeField] private Flock flockTwo;
        private Dictionary<int, List<FishSO>> fishDictionary = new Dictionary<int, List<FishSO>>();
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private List<Fish> fishListInGame = new List<Fish>();
        [SerializeField] private Counter maxFish;
        [SerializeField] private Counter startFishAmount;
        private Dictionary<FishTypes, int> fishTypesCounter = new Dictionary<FishTypes, int>();
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

        private void Start()
        {
            for (int i = 0; i < startFishAmount.counter; i++)
            {
                CreateFish(0);
                RandomizeSpawnPoint();
            }
        }

        public Dictionary<FishTypes, int> GetFishCount()
        {
            return fishTypesCounter;
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
            if (fishSO != null && fishListInGame.Count < maxFish.counter)
            {
                Fish fish = new Fish(fishSO.fishType, fishSO.name, fishSO.rarity, fishSO.description, fishSO.sprite);
                FishCustomization fishAttributes = GetFishAttributes(fishSO);
                GameObject obj = Instantiate(fishSO.prefab, spawnPoint.position, Quaternion.identity);
                obj.GetComponent<FishCustomizeManager>().CustomizeFish(fishAttributes);
                obj.GetComponent<FishReproductionManager>().SetFish(fish);
                Flock chosenFlock = GetRandomEvent();
                if (chosenFlock == flockOne)
                {
                    onFishSpawn.Raise(obj);
                }
                else
                {
                    onFishSpawnOtherFlock.Raise(obj);
                }
                obj.GetComponent<FishReproductionManager>().SetChosenFlock(chosenFlock);
                CountFishTypes(fish);
                fishListInGame.Add(fish);
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
            Sprite[] accessorySprites = fishSo.accessories;
            return new FishCustomization(bodyPatternSprite, tailSprite, topFinSprite, eyeSprite, bodyColor, otherColor, patternColor, accessorySprites);
        }
        
        private void CountFishTypes(Fish fish)
        {
            if (fishTypesCounter.ContainsKey(fish.FishType))
            {
                fishTypesCounter[fish.FishType]++;
            }
            else
            {
                fishTypesCounter.Add(fish.FishType, 1);
            }
        }

        private Flock GetRandomEvent()
        {
            if (Random.Range(0f, 1f) < 0.5f)
            {
                return flockOne;
            }
            return flockTwo;
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
            spawnPoint.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        }
    }
}