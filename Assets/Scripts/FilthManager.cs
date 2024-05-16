using System;
using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;
using Random = UnityEngine.Random;

public class FilthManager : ValidatedMonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private Counter filthTreshold;
    [SerializeField] private Cooldown filthCooldown;
    
    [Header("Filth Settings")]
    [SerializeField] private ObjectPool filthPool;
    [SerializeField] private Collider2D filthZone;
    
    [Header("Events")]
    [SerializeField] private OnFilthInvasion onFilthInvasion;
    [SerializeField] private OnFilthClean onFilthClean;
    [SerializeField, Self] private OnFilthRemovedListener onFilthRemovedListener;
    
    private float _currentCooldown;
    private int _filthCount;
    private bool _isFilthInvasion;

    private void OnEnable()
    {
        onFilthRemovedListener.Response.AddListener(RemoveFilth);
    }

    void Update()
    {
        _currentCooldown += Time.deltaTime;
        if (_currentCooldown >= filthCooldown.cooldownTime)
        {
            _currentCooldown = 0;
            CreateFilth();
        }
    }

    private void CreateFilth()
    {
        GameObject filth = filthPool.GetObject();
        filth.transform.position = GetRandomPointInCollider();
        _filthCount++;
        if (_filthCount >= filthTreshold.counter)
        {
            onFilthInvasion.Raise();
        }
    }
    
    private Vector2 GetRandomPointInCollider()
    {
        float x = Random.Range(filthZone.bounds.min.x, filthZone.bounds.max.x);
        float y = Random.Range(filthZone.bounds.min.y, filthZone.bounds.max.y);
        return new Vector2(x, y);
    }
    
    private void RemoveFilth()
    {
        _filthCount--;
        if (_filthCount < filthTreshold.counter)
        {
            onFilthClean.Raise();
        }
    }

    private void OnDisable()
    {
        onFilthRemovedListener.Response.RemoveListener(RemoveFilth);
    }
}
