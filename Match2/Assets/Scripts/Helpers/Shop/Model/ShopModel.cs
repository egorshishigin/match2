using System;

using Helpers.Config;

namespace Helpers.Shop.Model
{
    public class ShopModel
    {
        private GameData _gameData;

        private HelpersConfig _config;

        public ShopModel(GameData gameData, HelpersConfig config)
        {
            _gameData = gameData;

            _config = config;
        }

        public event Action ScoreSpent = delegate { };

        public void BuyHelper(int id)
        {
            HelperData helperData = _config.GetHelperByID(id);

            if (_gameData.GameScore >= helperData.Price)
            {
                _gameData.ChangeHelperCount(id, +1);

                _gameData.SpendScore(helperData.Price);

                ScoreSpent.Invoke();
            }

        }
    }
}