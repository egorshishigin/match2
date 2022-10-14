using System;

using Helpers.Shop.Model;

using GameStatistic.IO;
using GameStatistic.View;

using Level.Model;

namespace GameStatistic.Controller
{
    public class GameStatisticController : IDisposable
    {
        private GameStatisticView _statisticView;

        private GameStatisticData _gameStatistic;

        private GameStatisticIO _statisticIO;

        private LevelModel _level;

        private ShopModel _shopModel;

        public GameStatisticController(GameStatisticView statisticView, GameStatisticData gameStatistic, LevelModel level, GameStatisticIO gameStatisticIO, ShopModel shopModel)
        {
            _statisticView = statisticView;

            _gameStatistic = gameStatistic;

            _statisticIO = gameStatisticIO;

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
            _statisticIO.SaveData(_gameStatistic);

            UpdateGameScore();
        }

        private void UpdateLevelScore()
        {
            _statisticView.UpdateScoreText(_gameStatistic.LevelScore.ToString());
        }

        private void UpdateLevelText()
        {
            _statisticView.UpdateLevelText(_gameStatistic.GetLevelText());
        }

        private void UpdateGameScore()
        {
            _statisticView.UpdateScoreText(_gameStatistic.GameScore.ToString());
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
            _statisticIO.SaveData(_gameStatistic);
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