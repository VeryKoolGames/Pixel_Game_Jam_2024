using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.U2D;

[Serializable]
class Reward
{
    public string rewardName;
    public GameObject rewardPrefab;
    
    public Reward(string rewardName, GameObject rewardPrefab, Transform rewardSpawnLocation)
    {
        this.rewardName = rewardName;
        this.rewardPrefab = rewardPrefab;
    }
}

public class RewardManager : ValidatedMonoBehaviour
{
    [SerializeField] private List<Reward> rewards = new List<Reward>();
    [SerializeField, Self] private OnRewardUnlockedListener onRewardUnlockedListener;
    private int currentRewardIndex = 0;
    private Dictionary<int, Reward> rewardDictionary = new Dictionary<int, Reward>();

    private void Awake()
    {
        onRewardUnlockedListener.Response.AddListener(UnlockReward);
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
            reward.rewardPrefab.SetActive(true);
            reward.rewardPrefab.transform.localScale = Vector3.zero;
            reward.rewardPrefab.transform.DOScale(1, 1f);
            currentRewardIndex++;
        }
    }
}
