using System;

using Helpers.Shop.Model;
using Helpers.Inventory;

namespace Helpers
{
    public abstract class HelperBase : IDisposable
    {
        private InventoryData _inventoryData;

        private InventoryDataIO _inventoryDataIO;

        private HelperView _helperView;

        private ShopModel _shopModel;

        void IDisposable.Dispose()
        {
            _helperView.ButtonClicked -= UpdateData;

            _shopModel.ScoreSpent -= OnScoreSpent;
        }

        protected HelperBase(InventoryData inventoryData, InventoryDataIO inventoryDataIO, HelperView helperView, ShopModel shopModel)
        {
            _inventoryData = inventoryData;

            _inventoryDataIO = inventoryDataIO;

            _helperView = helperView;

            _shopModel = shopModel;

            Initialize();
        }

        protected abstract void UseHelper();

        private void UpdateData(string name)
        {
            if (_inventoryData.Helpers[name] > 0)
            {
                _inventoryData.ChangeHelperCount(name, -1);

                _inventoryDataIO.SaveData(_inventoryData);

                UseHelper();

                _helperView.UpdateCountText(_inventoryData.GetHelperCount(name));
            }
            else return;
        }

        private void Initialize()
        {
            _helperView.ButtonClicked += UpdateData;

            UpdateHelperCount();

            _shopModel.ScoreSpent += OnScoreSpent;
        }

        private void UpdateHelperCount()
        {
            _helperView.UpdateCountText(_inventoryData.GetHelperCount(_helperView.Name));
        }

        private void OnScoreSpent()
        {
            UpdateHelperCount();
        }
    }
}