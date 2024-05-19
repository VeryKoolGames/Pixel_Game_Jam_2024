using UnityEngine;

public class FishCustomization
{
    public Sprite patternSprite;
    public Sprite tailSprite;
    public Sprite finSprite;
    public Sprite eyeSprite;
    public Sprite[] accessorySprites;
    public Color bodyColor;
    public Color otherColor;
    public Color patternColor;

    public FishCustomization(Sprite patternSprite, Sprite tailSprite, Sprite finSprite, Sprite eyeSprite, Color bodyColor, Color otherColor, Color patternColor, Sprite[] accessorySprites)
    {
        this.patternSprite = patternSprite;
        this.tailSprite = tailSprite;
        this.finSprite = finSprite;
        this.eyeSprite = eyeSprite;
        this.bodyColor = bodyColor;
        this.otherColor = otherColor;
        this.patternColor = patternColor;
        this.accessorySprites = accessorySprites;
    }
}