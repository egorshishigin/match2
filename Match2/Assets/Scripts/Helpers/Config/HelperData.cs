using System;

using UnityEngine;

namespace Helpers.Config
{
    [Serializable]
    public class HelperData
    {
        [SerializeField] private int _id;

        [SerializeField] private string _name;

        [SerializeField] private int _price;

        [SerializeField] private Sprite _icon;

        public int ID => _id;

        public string Name => _name;

        public int Price => _price;

        public Sprite Icon => _icon;
    }
}