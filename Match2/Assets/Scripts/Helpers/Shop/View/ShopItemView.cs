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

        public string Name => _name.text;

        public event Action<string> ButtonClicked = delegate { };

        private void OnEnable()
        {
            _itemButton.onClick.AddListener(ButtonClick);
        }

        private void OnDisable()
        {
            _itemButton.onClick.RemoveListener(ButtonClick);
        }

        public void Initialize(string name, int price, Sprite icon)
        {
            _name.text = name;

            _price.text = price.ToString();

            _icon.sprite = icon;
        }

        public void UpdateCountText(int count)
        {
            _count.text = count.ToString();
        }

        private void ButtonClick()
        {
            ButtonClicked.Invoke(_name.text);
        }
    }
}