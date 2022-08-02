using System;

using UnityEngine;

[Serializable]
public class LevelPreset
{
    [SerializeField] private int _itemsCount;

    [SerializeField] private Vector3 _spawnOffsets;

    [SerializeField] private float _levelTime;

    public int ItemsCount => _itemsCount;

    public Vector3 SpawnOffsets => _spawnOffsets;

    public float CountdownTime => _levelTime;
}
