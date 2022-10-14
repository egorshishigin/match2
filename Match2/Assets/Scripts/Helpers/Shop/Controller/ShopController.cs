using System;
using System.Linq;
using System.Collections.Generic;

using Helpers.Config;
using Helpers.Shop.View;
using Helpers.Shop.Model;
using Helpers.Inventory;

using UnityEngine;
using UnityEngine.UI;

namespace Helpers.Shop.Controller
{
    public class ShopController : IDisposable
    {
        private HelpersConfig _config;

        private ShopItemView _itemViewTemplate;

        private Button _openShopButton;

        private Transform _itemsHolder;

        private ShopModel _shopModel;

        private InventoryData _inventoryData;

        private InventoryDataIO _dataIO;

        private List<ShopItemView> _itemViews = new List<ShopItemView>();

        public ShopController(HelpersConfig config, ShopItemView itemViewTemplate, Button openShopButton, Transform itemsHolder, ShopModel shopModel, InventoryData inventoryData, InventoryDataIO dataIO)
        {
            _config = config;

            _itemViewTemplate = itemViewTemplate;

            _openShopButton = openShopButton;

            _itemsHolder = itemsHolder;

            _shopModel = shopModel;

            _inventoryData = inventoryData;

            _dataIO = dataIO;

            InitializeViews();
        }

        void IDisposable.Dispose()
        {
            foreach (ShopItemView itemView in _itemViews)
                itemView.ButtonClicked -= OnItemButtonClicked;

            _openShopButton.onClick.RemoveListener(OpenShop);
        }

        private void OpenShop()
        {
            foreach (ShopItemView itemView in _itemViews)
            {
                UpdateView(itemView.Name);
            }
        }

        private void InitializeViews()
        {
            foreach (HelperData helperData in _config.Helpers)
            {
                ShopItemView view = GameObject.Instantiate(_itemViewTemplate, _itemsHolder);

                view.Initialize(helperData.Name, helperData.Price, helperData.Icon);

                view.ButtonClicked += OnItemButtonClicked;

                _itemViews.Add(view);
            }

            _openShopButton.onClick.AddListener(OpenShop);
        }

        private void OnItemButtonClicked(string name)
        {
            _shopModel.BuyHelper(name);

            UpdateView(name);

            _dataIO.SaveData(_inventoryData);
        }

        private void UpdateView(string name)
        {
            foreach (var itemView in _itemViews.Where(itemView => itemView.Name == name))
            {
                itemView.UpdateCountText(_inventoryData.GetHelperCount(name));
            }
        }
    }
}