using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using KBCore.Refs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FishCardUI
{
    public GameObject fishCard;
    public TextMeshProUGUI fishName;
    public FishTypes fishType;
}

public class AchievmentBook : ValidatedMonoBehaviour
{
    [SerializeField] private OnRewardUnlocked onRewardUnlocked;
    [SerializeField] private List<FishCardUI> fishCarsdUILigneOne;
    [SerializeField] private List<FishCardUI> fishCarsdUILigneTwo;
    [SerializeField] private List<FishCardUI> fishCarsdUILigneThree;
    private Dictionary<int, List<FishCardUI>> fishCardsUi;
    private Dictionary<int, int> amountUnlockedPerLines;
    [SerializeField] private List<GameObject> rewardButtons;
    private int cardUnlockedCounter;
    private int rewardUnlockedCounter;

    private void Awake()
    {
        amountUnlockedPerLines = new Dictionary<int, int>
        {
            {0, 0},
            {1, 0},
            {2, 0}
        };
        fishCardsUi = new Dictionary<int, List<FishCardUI>>
        {
            {0, fishCarsdUILigneOne},
            {1, fishCarsdUILigneTwo},
            {2, fishCarsdUILigneThree}
        };
    }

    public void OnAchievmentUnlocked(Fish fish)
    {
        CreateFishCard(fish);
    }
    
    private void CreateFishCard(Fish fish)
    {
        SetFishCardOnUi(fish);
    }

    public void UnlockRewardOne()
    {
        onRewardUnlocked.Raise(0);
    }
    
    public void UnlockRewardTwo()
    {
        onRewardUnlocked.Raise(1);
    }
    
    public void UnlockRewardThree()
    {
        onRewardUnlocked.Raise(2);
    }

    private void SetFishCardOnUi(Fish fish)
    {
        List<FishCardUI> fishCards = fishCardsUi[fish.FishRarety];

        foreach (var card in fishCards)
        {
            if (card.fishType == fish.FishType)
            {
                card.fishName.text = fish.FishName;
                card.fishCard.SetActive(true);
                amountUnlockedPerLines[fish.FishRarety]++;
                if (amountUnlockedPerLines[fish.FishRarety] == 4)
                {
                    rewardButtons[fish.FishRarety].SetActive(true);
                    rewardUnlockedCounter++;
                    if (rewardUnlockedCounter == 3)
                    {
                        FishCreator.Instance.SetSpawnUltimateFish(true);
                    }
                }
                break;
            }
        }
    }
}
