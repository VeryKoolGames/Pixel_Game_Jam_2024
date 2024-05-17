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
    public TextMeshProUGUI fishDescription;
    public Image fishSprite;
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
    private int cardUnlockedCounter;

    private void Start()
    {
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

    private void SetFishCardOnUi(Fish fish)
    {
        List<FishCardUI> fishCards = fishCardsUi[fish.FishRarety];
        if (fishCards.Count == 0)
        {
            return;
        }

        foreach (var card in fishCards)
        {
            if (card.fishType == fish.FishType)
            {
                card.fishSprite.sprite = fish.FishSprite;
                card.fishName.text = fish.FishName;
                card.fishDescription.text = fish.FishDescription;
                fishCardsUi[fish.FishRarety].Remove(card);
                if (fishCardsUi[fish.FishRarety].Count == 0)
                {
                    onRewardUnlocked.Raise(fish.FishRarety);
                }
            }
        }
    }
}
