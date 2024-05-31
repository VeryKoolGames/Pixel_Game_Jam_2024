using System;
using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class MainMenuFishCreator : ValidatedMonoBehaviour
    {
        public static MainMenuFishCreator Instance { get; private set; }
        [SerializeField] private List<FishSO> fishList = new List<FishSO>();
        [SerializeField] private OnFishSpawn onFishSpawn;
        [SerializeField] private OnFishSpawn onFishSpawnOtherFlock;
        [SerializeField, Self] private OnFishDeathListener onFishDeath;
        [SerializeField] private Flock flockOne;
        [SerializeField] private Flock flockTwo;
        private Dictionary<int, List<FishSO>> fishDictionary = new Dictionary<int, List<FishSO>>();
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform startGameSpawnPoint;
        private int fishListInGame;
        [SerializeField] private Counter maxFish;
        [SerializeField] private Counter startFishAmount;
        [SerializeField] private SpriteRenderer caveSprite;
        private Dictionary<FishTypes, int> fishTypesCounter = new Dictionary<FishTypes, int>();
        [SerializeField] private GameObject UltimateFish;
        private bool canSpawnUltimateFish;
        private bool hasSpawnedUltimateFish;
        private bool shouldSpawnRarerFish;
        
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

        public void OnStartSceneStart()
        {
            for (int i = 0; i < startFishAmount.counter; i++)
            {
                CreateFish(0);
                RandomizeSpawnPoint();
            }
        }

        public void OnGameSceneStart()
        {
            FishSO fishSO = GetFishSO(0);
            caveSprite.sortingOrder = 50;
            for (int i = 0; i < startFishAmount.counter; i++)
            {
                SpawnFishOnStart(fishSO);
            }
            StartCoroutine(AfterFishSpawn());
            StartCoroutine(WaitForFishSpawn());
        }
        
        private IEnumerator AfterFishSpawn()
        {
            yield return new WaitForSeconds(30f);
            shouldSpawnRarerFish = true;
        }

        private IEnumerator WaitForFishSpawn()
        {
            yield return new WaitForSeconds(2);
            caveSprite.sortingOrder = 2;
        }

        private void Start()
        {
            onFishDeath.Response.AddListener(OnFishDeath);
        }
        
        public void SetSpawnUltimateFish(bool value)
        {
            canSpawnUltimateFish = value;
        }

        public Dictionary<FishTypes, int> GetFishCount()
        {
            return fishTypesCounter;
        }

        private void OnFishDeath(FlockAgent arg0)
        {
            fishListInGame--;
            if (fishListInGame == 0)
            {
                OnGameSceneStart();
            }
        }
        

        public void CreateFish(Fish fishOne, Fish fishTwo)
        {
            if (canSpawnUltimateFish && !hasSpawnedUltimateFish && Random.Range(0, 100) < 30)
            {
                SpawnUltimateFish();
            }
            else
            {
                int rarity = GetFishRarity(fishOne.FishRarety, fishTwo.FishRarety);
                FishSO fishSO = GetFishSO(rarity);
                SpawnFish(fishSO);
            }
        }

        private void SpawnFishOnStart(FishSO fishSO)
        {
            if (fishSO != null && fishListInGame < maxFish.counter)
            {
                Fish fish = new Fish(fishSO.fishType, fishSO.name, fishSO.rarity, fishSO.description, fishSO.sprite);
                FishCustomization fishAttributes = GetFishAttributes(fishSO);
                GameObject obj = Instantiate(fishSO.prefab, startGameSpawnPoint.position, Quaternion.identity);
                obj.GetComponent<FishCustomizeManager>().CustomizeFish(fishAttributes);
                obj.GetComponent<FishReproductionManager>().SetFish(fish);
                Flock chosenFlock = GetRandomEvent();
                obj.GetComponent<FishReproductionManager>().SetChosenFlock(chosenFlock);
                obj.GetComponent<FishReproductionManager>().OnGameStart();
                CountFishTypes(fish);
                fishListInGame++;
            }
        }

        private void SpawnUltimateFish()
        {
            UltimateFish.SetActive(true);
        }
        
        public void CreateFish(FishSO fishSO)
        {
            SpawnFish(fishSO);
        }

        public void RaiseFishEvent(Flock chosenFlock, GameObject obj)
        {
            if (chosenFlock == flockOne)
            {
                onFishSpawn.Raise(obj);
            }
            else
            {
                onFishSpawnOtherFlock.Raise(obj);
            }
        }
        
        private void SpawnFish(FishSO fishSO)
        {
            if (fishSO != null && fishListInGame < maxFish.counter)
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
                fishListInGame++;
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
            if (shouldSpawnRarerFish && Random.Range(0, 1f) < 0.2f)
            {
                return 2;
            }
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

        public Dictionary<FishTypes, int> GetEndingData()
        {
            return fishTypesCounter;
        }

    }
}