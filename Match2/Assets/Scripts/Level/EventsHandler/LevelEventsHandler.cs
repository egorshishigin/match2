using System.Runtime.InteropServices;
using System;

using Booster;

using Level.Model;
using Level.View;

using PlayerInput;

namespace Level.EventsHandler
{
    public class LevelEventsHandler : IDisposable
    {
        [DllImport("__Internal")]
        private static extern void SetLB(int value);

        private LevelModel _level;

        private GameData _gameData;

        private ScoreBooster _scoreBooster;

        private LevelMenuView _menuView;

        private RigibodyMouseDrag _playerControl;

        public LevelEventsHandler(LevelModel level, GameData gameData, ScoreBooster scoreBooster, LevelMenuView menuView, RigibodyMouseDrag playerControl)
        {
            _level = level;

            _gameData = gameData;

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
            ShowAD();

            _playerControl.enabled = false;

            _menuView.TimeIsUp();
        }

        private void OnLevelStarted()
        {
            _gameData.ResetLevelScore();

            _scoreBooster.ResetBooster();
        }

        private void OnLevelCompleted()
        {
            _gameData.LevelUp();

            _gameData.IncreaseGameScore();

            _menuView.ShowLevelScore(_gameData.LevelScore.ToString());

            _menuView.LevelCompleted(_gameData.Level);

            ShowAD();

            Game.Instance.SaveData();

            SetLB(_gameData.Level);
        }

        private void ShowAD()
        {
            if (_gameData.Level % 5 == 0)
            {
                Game.Instance.ShowAD();
            }
        }

        private void OnScoreUp()
        {
            _gameData.ScoreUp(_scoreBooster.ScoreBoostAmount);
        }
    }
}