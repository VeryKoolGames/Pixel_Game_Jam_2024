using System;
using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

[Serializable]
class Reward
{
    public string rewardName;
    public GameObject rewardPrefab;
    public Transform rewardSpawnLocation;
    
    public Reward(string rewardName, GameObject rewardPrefab, Transform rewardSpawnLocation)
    {
        this.rewardName = rewardName;
        this.rewardPrefab = rewardPrefab;
        this.rewardSpawnLocation = rewardSpawnLocation;
    }
}

public class RewardManager : ValidatedMonoBehaviour
{
    [SerializeField] private List<Reward> rewards = new List<Reward>();
    [SerializeField, Self] private OnRewardUnlockedListener onRewardUnlockedListener;
    private int currentRewardIndex = 0;
    [SerializeField] private Dictionary<int, Reward> rewardDictionary = new Dictionary<int, Reward>();

    private void Awake()
    {
        onRewardUnlockedListener.Response.AddListener(UnlockReward);
    }
    
    private void Start()
    {
        foreach (var reward in rewards)
        {
            rewardDictionary.Add(rewards.IndexOf(reward), reward);
        }
    }

    // Start is called before the first frame update
    public void UnlockReward(int rarity)
    {
        if (currentRewardIndex < rewards.Count)
        {
            Reward reward = rewardDictionary[rarity];
            Instantiate(reward.rewardPrefab, reward.rewardSpawnLocation.position, Quaternion.identity);
            currentRewardIndex++;
        }
    }
}
