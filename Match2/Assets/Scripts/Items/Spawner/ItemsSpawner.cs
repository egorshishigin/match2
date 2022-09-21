using System.Collections.Generic;

using Items.Config;

using Level.Config;

using UnityEngine;

namespace Items.Spawner
{
    public class ItemsSpawner
    {
        private ItemsConfig _config;

        private float _xSpawnOffset;

        private float _ySpawnOffset;

        private float _zSpawnOffset;

        private List<Item> _randomItems = new List<Item>();

        private List<GameObject> _spawnedItems = new List<GameObject>();

        public ItemsSpawner(ItemsConfig itemsConfig)
        {
            _config = itemsConfig;
        }

        public List<GameObject> Items => _spawnedItems;

        public void SpawnRandomItems(LevelPreset levelPreset)
        {
            _xSpawnOffset = levelPreset.SpawnOffsets.x;

            _ySpawnOffset = levelPreset.SpawnOffsets.y;

            _zSpawnOffset = levelPreset.SpawnOffsets.z;

            GetRandomItems(levelPreset.ItemsCount);

            if (_randomItems == null)
            {
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < _randomItems.Count; j++)
                {
                    float x = Random.Range(-_xSpawnOffset, _xSpawnOffset);

                    float y = Random.Range(0, _ySpawnOffset);

                    float z = Random.Range(-_zSpawnOffset, _zSpawnOffset);

                    Vector3 spawnPosition = new Vector3(x, y, z);

                    GameObject spawnedObject = Object.Instantiate(_randomItems[j].gameObject, spawnPosition, Quaternion.identity);

                    _spawnedItems.Add(spawnedObject);
                }
            }
        }

        public void DestroyItems()
        {
            if (_spawnedItems == null)
                return;

            foreach (GameObject spawnedItem in _spawnedItems)
            {
                Object.Destroy(spawnedItem);
            }
        }

        public void ClearRandomItems()
        {
            _randomItems.Clear();
        }

        private List<Item> GetRandomItems(int count)
        {
            if (count > _config.Items.Count)
            {
                Debug.LogWarning("Items count to spawn more than size of collection");

                return null;
            }

            for (int i = 0; i < count; i++)
            {
                int randomIndex;

                do
                {
                    randomIndex = Random.Range(0, _config.Items.Count);
                }
                while (_randomItems.Contains(_config.Items[randomIndex]));

                _randomItems.Add(_config.Items[randomIndex]);
            }
            return _randomItems;
        }
    }
}