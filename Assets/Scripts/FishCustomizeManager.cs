using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCustomizeManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer tailRenderer;
    [SerializeField] private SpriteRenderer finRenderer;
    [SerializeField] private SpriteRenderer eyeRenderer;
    [SerializeField] private SpriteRenderer patternRenderer;
    [SerializeField] private SpriteRenderer accessoryRenderer;
    [SerializeField] private Sprite[] accessorySprites;
    
    public void CustomizeFish(FishCustomization fishCustomization)
    {
        bodyRenderer.color = fishCustomization.bodyColor;
        tailRenderer.sprite = fishCustomization.tailSprite;
        finRenderer.sprite = fishCustomization.finSprite;
        eyeRenderer.sprite = fishCustomization.eyeSprite;
        if (patternRenderer)
        {
            patternRenderer.sprite = fishCustomization.patternSprite;
            patternRenderer.color = fishCustomization.patternColor;
        }
        finRenderer.color = fishCustomization.otherColor;
        tailRenderer.color = fishCustomization.otherColor;
    }
    
    public void ModifyFishAccessory()
    {
        if (accessorySprites.Length == 0)
        {
            return;
        }
        Sprite accessorySprite = accessorySprites[Random.Range(0, accessorySprites.Length)];
        accessoryRenderer.sprite = accessorySprite;
    }
}