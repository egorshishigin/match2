using System;

using DG.Tweening;

using Items.MatcherTrigger;
using Items.Spawner;

using Level.Config;
using Level.Configurator;

using Timer;

namespace Level.Model
{
    public class LevelModel : IDisposable
    {
        private ItemsMatcherTrigger _matcherTrigger;

        private ItemsSpawner _spawner;

        private CountdownTimer _timer;

        private int _itemsCountToWin;

        private int _currentMatchedItems;

        private LevelConfigurator _levelConfigurator;


        public LevelModel(ItemsMatcherTrigger matcherTrigger, ItemsSpawner spawner, LevelConfigurator levelConfigurator, CountdownTimer timer)
        {
            _matcherTrigger = matcherTrigger;

            _spawner = spawner;

            _levelConfigurator = levelConfigurator;

            _timer = timer;

            Initialize();
        }

        public event Action LevelStarted = delegate { };

        public event Action ScoreUp = delegate { };

        public event Action LevelCompleted = delegate { };

        public event Action LevelLosed = delegate { };

        void IDisposable.Dispose()
        {
            _matcherTrigger.ItemsMatch.RemoveListener(OnItemsMatch);

            _timer.TimeIsUp -= OnTimeIsUp;
        }

        public void StartLevel()
        {
            _matcherTrigger.ClearTriggerItems();

            _spawner.DestroyItems();

            _spawner.ClearRandomItems();

            LevelPreset levelPreset = _levelConfigurator.SetUpLevelPreset();

            SetCountToWin(levelPreset.ItemsCount);

            _spawner.SpawnRandomItems(levelPreset);

            _timer.StartTimer(levelPreset.CountdownTime);

            LevelStarted.Invoke();
        }

        public void PauseGame()
        {
            _timer.StopTimer();
        }

        public void ClearItemsObjects()
        {
            _spawner.DestroyItems();

            _spawner.ClearRandomItems();

            _matcherTrigger.ClearTriggerItems();
        }

        private void OnTimeIsUp()
        {
            _timer.StopTimer();

            LevelLosed.Invoke();
        }

        private void Initialize()
        {
            _matcherTrigger.ItemsMatch.AddListener(OnItemsMatch);

            _timer.TimeIsUp += OnTimeIsUp;
        }

        private void SetCountToWin(int countToWin)
        {
            _currentMatchedItems = 0;

            _itemsCountToWin = countToWin;
        }

        private void OnItemsMatch()
        {
            ScoreUp.Invoke();

            _currentMatchedItems++;

            if (_currentMatchedItems == _itemsCountToWin)
            {
                CompleteLevel();
            }
        }

        private void CompleteLevel()
        {
            _matcherTrigger.Sequence.OnComplete(() =>
            {
                _spawner.DestroyItems();
            });

            _spawner.ClearRandomItems();

            _matcherTrigger.ClearTriggerItems();

            _timer.StopTimer();

            LevelCompleted.Invoke();
        }
    }
}