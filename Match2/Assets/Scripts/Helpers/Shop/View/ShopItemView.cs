using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Helpers.Shop.View
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;

        [SerializeField] private TMP_Text _price;

        [SerializeField] private Image _icon;

        [SerializeField] TMP_Text _count;

        [SerializeField] private Button _itemButton;

        private int _id;

        public int ID => _id;

        public event Action<int> ButtonClicked = delegate { };

        private void OnDestroy()
        {
            _itemButton.onClick.RemoveListener(ButtonClick);
        }

        public void Initialize(int id, string name, int price, Sprite icon)
        {
            _id = id;

            _name.text = name;

            _price.text = price.ToString();

            _icon.sprite = icon;

            _itemButton.onClick.AddListener(ButtonClick);
        }

        public void UpdateCountText(int count)
        {
            _count.text = count.ToString();
        }

        private void ButtonClick()
        {
            ButtonClicked.Invoke(_id);
        }
    }
}