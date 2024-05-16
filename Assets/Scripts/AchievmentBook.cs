using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using KBCore.Refs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FishCard
{
    public string fishName;
    public Sprite fishSprite;
    public string fishDescription;
    
    public FishCard(string fishName, Sprite fishSprite, string fishDescription)
    {
        this.fishName = fishName;
        this.fishSprite = fishSprite;
        this.fishDescription = fishDescription;
    }
}

[Serializable]
public class FishCardUI
{
    public TextMeshProUGUI fishDescription;
    public Image fishSprite;
    public TextMeshProUGUI fishName;
}

public class AchievmentBook : ValidatedMonoBehaviour
{
    [SerializeField] private OnRewardUnlocked onRewardUnlocked;
    [SerializeField] private List<FishCard> fishCards;
    [SerializeField] private List<FishCardUI> fishCarsdUI;
    
    public void OnAchievmentUnlocked(Fish fish)
    {
        CreateFishCard(fish);
    }
    
    private void CreateFishCard(Fish fish)
    {
        FishCard bookPage = new FishCard(fish.FishName, fish.FishSprite, fish.FishName);
        fishCards.Add(bookPage);
        SetFishCardOnUi();
        if (fishCards.Count % 3 == 0)
        {
            onRewardUnlocked.Raise();
        }
    }

    private void SetFishCardOnUi()
    {
        FishCardUI fishCardUi = fishCarsdUI[fishCards.Count - 1];
        FishCard fishCard = fishCards[fishCards.Count - 1];
            
        fishCardUi.fishSprite.sprite = fishCard.fishSprite;
        fishCardUi.fishName.text = fishCard.fishName;
        fishCardUi.fishDescription.text = fishCard.fishDescription;
    }
}
