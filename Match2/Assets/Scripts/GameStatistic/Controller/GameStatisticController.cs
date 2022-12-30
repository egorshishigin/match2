using System;

using Helpers.Shop.Model;

using GameStatistic.View;

using Level.Model;

namespace GameStatistic.Controller
{
    public class GameStatisticController : IDisposable
    {
        private GameStatisticView _statisticView;

        private GameData _gameData;

        private LevelModel _level;

        private ShopModel _shopModel;

        public GameStatisticController(GameStatisticView statisticView, GameData gameData, LevelModel level, ShopModel shopModel)
        {
            _statisticView = statisticView;

            _gameData = gameData;

            _level = level;

            _shopModel = shopModel;

            Initialize();
        }

        void IDisposable.Dispose()
        {
            _level.LevelCompleted -= OnLevelCompleted;

            _level.ScoreUp -= OnScoreUp;

            _level.LevelStarted -= OnLevelStarted;

            _shopModel.ScoreSpent -= OnScoreSpent;

            _statisticView.HomeButton.onClick.RemoveListener(OnHomeButtonClick);
        }

        private void Initialize()
        {
            _level.LevelCompleted += OnLevelCompleted;

            _level.ScoreUp += OnScoreUp;

            _level.LevelStarted += OnLevelStarted;

            _shopModel.ScoreSpent += OnScoreSpent;

            _statisticView.HomeButton.onClick.AddListener(OnHomeButtonClick);

            UpdateGameScore();

            UpdateLevelText();
        }

        private void OnScoreSpent()
        {
            Game.Instance.SaveData();

            UpdateGameScore();
        }

        private void UpdateLevelScore()
        {
            _statisticView.UpdateScoreText(_gameData.LevelScore.ToString());
        }

        private void UpdateLevelText()
        {
            _statisticView.UpdateLevelText(_gameData.GetLevelText());
        }

        private void UpdateGameScore()
        {
            _statisticView.UpdateScoreText(_gameData.GameScore.ToString());
        }

        private void OnLevelStarted()
        {
            UpdateLevelScore();
        }

        private void OnScoreUp()
        {
            UpdateLevelScore();
        }

        private void OnLevelCompleted()
        {
            Game.Instance.SaveData();
        }

        private void OnHomeButtonClick()
        {
            UpdateGameScore();

            UpdateLevelText();

            DestroyItems();
        }

        private void DestroyItems()
        {
            _level.ClearItemsObjects();
        }
    }
}