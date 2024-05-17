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
    
    public void CustomizeFish(FishCustomization fishCustomization)
    {
        bodyRenderer.color = fishCustomization.bodyColor;
        tailRenderer.sprite = fishCustomization.tailSprite;
        finRenderer.sprite = fishCustomization.finSprite;
        eyeRenderer.sprite = fishCustomization.eyeSprite;
        patternRenderer.sprite = fishCustomization.patternSprite;
        patternRenderer.color = fishCustomization.patternColor;
        finRenderer.color = fishCustomization.otherColor;
        tailRenderer.color = fishCustomization.otherColor;
    }
}