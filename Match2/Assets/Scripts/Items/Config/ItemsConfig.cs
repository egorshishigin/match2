using System.Collections.Generic;

using UnityEngine;

namespace Items.Config
{
    [CreateAssetMenu(fileName = "ItemsConfig", menuName = "ScriptableObjects/ItemsConfig")]
    public class ItemsConfig : ScriptableObject
    {
        [SerializeField] private List<Item> _items;

        public List<Item> Items => _items;
    }
}