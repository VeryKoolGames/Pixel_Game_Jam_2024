using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.U2D;

public class WaterColorManager : ValidatedMonoBehaviour
{
    [SerializeField] private Color dirtyWaterColor;
    [SerializeField] private SpriteShapeRenderer waterMaterial;

    [SerializeField] private GameObject smellParticles;
    [SerializeField] private OnFilthInvasion onFilthInvasion;
    [SerializeField, Self] private OnFilthRemovedListener onFilthRemovedListener;
    [SerializeField] private Cooldown waterCooldown;
    private Tween currentTween;
    private Tween currentCleanTween;
    [SerializeField] private OnFilthInvasion onWaterDirty;
    [SerializeField] private OnFilthClean onWaterClean;

    private Color baseColor;
    // Start is called before the first frame update
    void Start()
    {
        baseColor = waterMaterial.material.color;
        SetDirtyWaterColor();
        onFilthRemovedListener.Response.AddListener(SetCleanWaterColor);
    }

    public void SetDirtyWaterColor()
    {
        currentTween = waterMaterial.material.DOColor(dirtyWaterColor, waterCooldown.cooldownTime).OnComplete(() =>
        {
            smellParticles.SetActive(true);
            onWaterDirty.Raise();
        });
    }
    
    public void SetCleanWaterColor()
    {
        currentTween?.Kill();
        currentCleanTween?.Kill();
        currentCleanTween = waterMaterial.material.DOColor(baseColor, 5f).OnComplete(() =>
        {
            smellParticles.SetActive(false);
            SetDirtyWaterColor();
            onWaterClean.Raise();
        });
    }
}
