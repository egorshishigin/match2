using System;

using Booster;

using GameStatistic;

using Level.Model;
using Level.View;

using PlayerInput;

namespace Level.EventsHandler
{
    public class LevelEventsHandler : IDisposable
    {
        private LevelModel _level;

        private GameStatisticData _gameStatistic;

        private ScoreBooster _scoreBooster;

        private LevelMenuView _menuView;

        private RigibodyMouseDrag _playerControl;

        public LevelEventsHandler(LevelModel level, GameStatisticData gameStatistic, ScoreBooster scoreBooster, LevelMenuView menuView, RigibodyMouseDrag playerControl)
        {
            _level = level;

            _gameStatistic = gameStatistic;

            _scoreBooster = scoreBooster;

            _menuView = menuView;

            _playerControl = playerControl;

            Initialize();
        }

        void IDisposable.Dispose()
        {
            _level.LevelStarted -= OnLevelStarted;

            _level.ScoreUp -= OnScoreUp;

            _level.LevelCompleted -= OnLevelCompleted;

            _level.LevelLosed -= OnLevelLosed;
        }

        private void Initialize()
        {
            _level.LevelStarted += OnLevelStarted;

            _level.ScoreUp += OnScoreUp;

            _level.LevelCompleted += OnLevelCompleted;

            _level.LevelLosed += OnLevelLosed;
        }

        private void OnLevelLosed()
        {
            _playerControl.enabled = false;

            _menuView.TimeIsUp();
        }

        private void OnLevelStarted()
        {
            _gameStatistic.ResetLevelScore();

            _scoreBooster.ResetBooster();
        }

        private void OnLevelCompleted()
        {
            _gameStatistic.LevelUp();

            _gameStatistic.IncreaseGameScore();

            _menuView.ShowLevelScore(_gameStatistic.LevelScore.ToString());

            _gameStatistic.ResetLevelScore();

            _menuView.LevelCompleted(_gameStatistic.Level);
        }

        private void OnScoreUp()
        {
            _gameStatistic.ScoreUp(_scoreBooster.ScoreBoostAmount);
        }
    }
}