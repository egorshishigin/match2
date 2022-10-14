using GameStatistic;

using Helpers.Config;
using Helpers.Inventory;
using System;

namespace Helpers.Shop.Model
{
    public class ShopModel
    {
        private InventoryData _inventoryData;

        private GameStatisticData _gameStatistic;

        private HelpersConfig _config;

        public ShopModel(InventoryData inventoryData, GameStatisticData gameStatistic, HelpersConfig config)
        {
            _inventoryData = inventoryData;

            _gameStatistic = gameStatistic;

            _config = config;
        }

        public event Action ScoreSpent = delegate { };

        public void BuyHelper(string name)
        {
            HelperData helperData = _config.GetHelperByName(name);

            if(_gameStatistic.GameScore >= helperData.Price)
            {
                _inventoryData.ChangeHelperCount(name, +1);

                _gameStatistic.SpendScore(helperData.Price);

                ScoreSpent.Invoke();
            }
            
        }
    }
}