using DG.Tweening;

using UnityEngine;

public class StartUpLevel : MonoBehaviour
{
    [SerializeField] private ItemsMatcherTrigger _matcherTrigger;

    [SerializeField] private ItemsSpawner _spawner;

    [SerializeField] private ItemsConfig _itemsConfig;

    [SerializeField] private LevelPresetsConfig _levelPresetsConfig;

    [SerializeField] private LevelCompletedView _levelCompletedView;

    [SerializeField] private string _levelCompletedText;

    [SerializeField] private string _timeIsUpText;

    [SerializeField] private CountdownTimer _timer;

    [SerializeField] private GameStartUp _game;

    [SerializeField] private GameObject _restartButton;

    [SerializeField] private GameObject _nextLevelButton;

    private LevelStatistic _levelStatistic;

    private LevelPreset _levelPreset;

    private void OnEnable()
    {
        _levelStatistic.LevelCompleted += OnLevelCompleted;

        _timer.TimeIsUp += OnTimeIsUp;
    }

    private void OnDisable()
    {
        _levelStatistic.LevelCompleted -= OnLevelCompleted;

        _timer.TimeIsUp -= OnTimeIsUp;
    }

    public void StartGame()
    {
        SetUpLevelPreset();

        _spawner = new ItemsSpawner();

        _levelStatistic = new LevelStatistic(_matcherTrigger);
    }

    public void NextLevel()
    {
        SetUpLevelPreset();

        SetUpLevel();
    }

    public void RestartLevel()
    {
        _matcherTrigger.ClearTriggerItems();

        _spawner.DestroyItems();

        _spawner.ClearRandomItems();

        SetUpLevel();
    }

    public void OpenMainMenu()
    {
        _game.UpdateStatistic();

        _matcherTrigger.ClearTriggerItems();

        _spawner.DestroyItems();

        _spawner.ClearRandomItems();

        _timer.StopTimer();

        _timer.enabled = false;
    }

    private void SetUpLevelPreset()
    {
        _levelPreset = _levelPresetsConfig.GetRandomLevelPreset();
    }

    private void SetUpLevel()
    {
        _spawner.SetLevelPreset(_itemsConfig, _levelPreset.ItemsCount, _levelPreset.SpawnOffsets);

        _levelStatistic.SetCountToWin(_levelPreset.ItemsCount);

        _timer.enabled = true;

        _timer.StartTimer(_levelPreset.CountdownTime);

        _spawner.SpawnRandomItems();
    }

    private void OnTimeIsUp()
    {
        _levelCompletedView.ShowElement(_timeIsUpText);

        _timer.StopTimer();

        _timer.enabled = false;

        _restartButton.SetActive(true);

        _nextLevelButton.SetActive(false);
    }

    private void OnLevelCompleted()
    {
        _levelCompletedView.ShowElement(_levelCompletedText);

        _matcherTrigger.Sequence.OnComplete(() =>
        {
            _spawner.DestroyItems();
        });

        _spawner.ClearRandomItems();

        _matcherTrigger.ClearTriggerItems();

        _timer.StopTimer();

        _timer.enabled = false;

        _nextLevelButton.SetActive(true);

        _restartButton.SetActive(false);

        _game.GameStatisticModel.LevelUp();

        _game.GameStatisticIO.SaveData(_game.GameStatisticModel);
    }
}
