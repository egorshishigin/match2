using System;
using System.Linq;
using System.Collections.Generic;

using Helpers.Config;
using Helpers.Shop.View;
using Helpers.Shop.Model;

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

        private GameData _gameData;

        private List<ShopItemView> _itemViews = new List<ShopItemView>();

        public ShopController(HelpersConfig config, ShopItemView itemViewTemplate, Button openShopButton, Transform itemsHolder, ShopModel shopModel, GameData gameData)
        {
            _config = config;

            _itemViewTemplate = itemViewTemplate;

            _openShopButton = openShopButton;

            _itemsHolder = itemsHolder;

            _shopModel = shopModel;

            _gameData = gameData;

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
                UpdateView(itemView.ID);
            }
        }

        private void InitializeViews()
        {
            foreach (HelperData helperData in _config.Helpers)
            {
                ShopItemView view = GameObject.Instantiate(_itemViewTemplate, _itemsHolder);

                view.Initialize(helperData.ID, helperData.Name, helperData.Price, helperData.Icon);

                view.ButtonClicked += OnItemButtonClicked;

                _itemViews.Add(view);
            }

            //_openShopButton.onClick.AddListener(OpenShop);
        }

        private void OnItemButtonClicked(int id)
        {
            _shopModel.BuyHelper(id);

            UpdateView(id);

            Game.Instance.SaveData();
        }

        private void UpdateView(int id)
        {
            foreach (var itemView in _itemViews.Where(itemView => itemView.ID == id))
            {
                itemView.UpdateCountText(_gameData.GetHelperCount(id));
            }
        }
    }
}