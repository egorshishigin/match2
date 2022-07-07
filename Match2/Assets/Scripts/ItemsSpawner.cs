using System.Collections.Generic;

using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private ItemsConfig _config;

    [SerializeField] private int _count;

    [SerializeField] private float _xSpawnOffset;

    [SerializeField] private float _ySpawnOffset;

    [SerializeField] private float _zSpawnOffset;

    private List<Item> _randomItems = new List<Item>();

    private void Start()
    {
        Application.targetFrameRate = 60;

        SpawnRandomItems();
    }

    private void SpawnRandomItems()
    {
        GetRandomItems(_count);

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < _randomItems.Count; j++)
            {
                float x = Random.Range(-_xSpawnOffset, _xSpawnOffset);

                float y = Random.Range(0, _ySpawnOffset);

                float z = Random.Range(-_zSpawnOffset, _zSpawnOffset);

                Vector3 spawnPosition = new Vector3(x, y, z);

                GameObject spawnedObject = Instantiate(_randomItems[j].gameObject, spawnPosition, Quaternion.identity);
            }
        }
    }

    private List<Item> GetRandomItems(int count)
    {
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
