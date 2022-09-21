using System;

using UnityEngine;

namespace Level.Config
{
    [Serializable]
    public class LevelPreset
    {
        [SerializeField] private int _difficulty;

        [SerializeField] private int _itemsCount;

        [SerializeField] private Vector3 _spawnOffsets;

        [SerializeField] private float _levelTime;

        public int Difficulty => _difficulty;

        public int ItemsCount => _itemsCount;

        public Vector3 SpawnOffsets => _spawnOffsets;

        public float CountdownTime => _levelTime;
    }
}