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

    private void Awake()
    {
        onRewardUnlockedListener.Response.AddListener(UnlockReward);
    }

    // Start is called before the first frame update
    public void UnlockReward()
    {
        if (currentRewardIndex < rewards.Count)
        {
            Reward reward = rewards[currentRewardIndex];
            Instantiate(reward.rewardPrefab, reward.rewardSpawnLocation.position, Quaternion.identity);
            currentRewardIndex++;
        }
    }
}
