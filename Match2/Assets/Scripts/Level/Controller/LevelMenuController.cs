﻿using System;

using Level.Model;
using Level.View;

using PlayerInput;

namespace Level.Controller
{
    public class LevelMenuController : IDisposable
    {
        private LevelMenuView _menuView;

        private LevelModel _level;

        private RigibodyMouseDrag _playerControl;

        public LevelMenuController(LevelMenuView menuView, LevelModel level, RigibodyMouseDrag playerControl)
        {
            _menuView = menuView;

            _level = level;

            _playerControl = playerControl;

            Inizialize();
        }

        void IDisposable.Dispose()
        {
            _menuView.StartGameButton.onClick.RemoveListener(StartNewLevel);

            _menuView.NextLevelButton.onClick.RemoveListener(StartNewLevel);

            _menuView.RestartButton.onClick.RemoveListener(StartNewLevel);

            _menuView.PauseClicked -= OnPauseClicked;

            _menuView.ResumeButton.onClick.RemoveListener(HideUI);

            _menuView.ExtraStarsButton.onClick.RemoveListener(ExtraStarsHandler);

            Game.Instance.GameData.ExtraStarsGiven -= OnExtraStarsGiven;
        }

        private void Inizialize()
        {
            _menuView.StartGameButton.onClick.AddListener(StartNewLevel);

            _menuView.NextLevelButton.onClick.AddListener(StartNewLevel);

            _menuView.RestartButton.onClick.AddListener(StartNewLevel);

            _menuView.PauseClicked += OnPauseClicked;

            _menuView.ResumeButton.onClick.AddListener(HideUI);

            _menuView.ExtraStarsButton.onClick.AddListener(ExtraStarsHandler);

            Game.Instance.GameData.ExtraStarsGiven += OnExtraStarsGiven;
        }

        private void OnExtraStarsGiven(int starsCount)
        {
            _menuView.ShowLevelScore(starsCount.ToString());
        }

        private void ExtraStarsHandler()
        {
            Game.Instance.PlayStarsAD();
        }

        private void OnPauseClicked()
        {
            _level.PauseGame();
        }

        private void StartNewLevel()
        {
            _level.StartLevel();

            EnablePLayerControl();

            HideUI();
        }

        private void HideUI()
        {
            _menuView.HideElement();
        }

        private void EnablePLayerControl()
        {
            _playerControl.enabled = true;
        }
    }
}