using System;

using Helpers.Shop.Model;

namespace Helpers
{
    public abstract class HelperBase : IDisposable
    {
        private GameData _gameData;

        private HelperView _helperView;

        private ShopModel _shopModel;

        void IDisposable.Dispose()
        {
            _helperView.ButtonClicked -= UpdateData;

            _shopModel.ScoreSpent -= OnScoreSpent;
        }

        protected HelperBase(GameData gameData, HelperView helperView, ShopModel shopModel)
        {
            _gameData = gameData;

            _helperView = helperView;

            _shopModel = shopModel;

            Initialize();
        }

        protected abstract void UseHelper();

        private void UpdateData(int id)
        {
            if (_gameData.Helpers[id] > 0)
            {
                _gameData.ChangeHelperCount(id, -1);

                Game.Instance.SaveData();

                UseHelper();

                _helperView.UpdateCountText(_gameData.GetHelperCount(id));
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
            _helperView.UpdateCountText(_gameData.GetHelperCount(_helperView.ID));
        }

        private void OnScoreSpent()
        {
            UpdateHelperCount();
        }
    }
}