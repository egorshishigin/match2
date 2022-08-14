using DG.Tweening;

using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ItemsMatcherTrigger _matcherTrigger;

    [SerializeField] private ItemsSpawner _spawner;

    [SerializeField] private ItemsConfig _itemsConfig;

    [SerializeField] private LevelPresetsConfig _levelPresetsConfig;

    [SerializeField] private AnimationCurve _difficultyFormLevel;

    [SerializeField] private LevelMenuView _levelMenuView;

    [SerializeField] private CountdownTimer _timer;

    [SerializeField] private GameStatisticController _gameStatistic;

    private LevelStatistic _levelStatistic;

    private LevelPreset _levelPreset;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        _levelStatistic.LevelCompleted += OnLevelCompleted;

        _levelStatistic.ScoreUp += OnScoreUp;

        _timer.TimeIsUp += OnTimeIsUp;
    }

    private void OnDisable()
    {
        _levelStatistic.LevelCompleted -= OnLevelCompleted;

        _levelStatistic.ScoreUp -= OnScoreUp;

        _timer.TimeIsUp -= OnTimeIsUp;
    }

    public void StartGame()
    {
        _gameStatistic.GameStatistic.ResetLevelScore();

        NextLevel();
    }

    public void NextLevel()
    {
        _gameStatistic.GameStatistic.ResetLevelScore();

        _gameStatistic.UpdateLevelScore();

        _spawner.DestroyItems();

        SetUpLevelPreset();

        SetUpLevel();
    }

    public void RestartLevel()
    {
        _matcherTrigger.ClearTriggerItems();

        _spawner.DestroyItems();

        _spawner.ClearRandomItems();

        _gameStatistic.GameStatistic.ResetLevelScore();

        _gameStatistic.UpdateLevelScore();

        SetUpLevel();
    }

    public void OpenMainMenu()
    {
        _gameStatistic.UpdateGameScore();

        _gameStatistic.UpdateLevelText();

        _gameStatistic.GameStatistic.ResetLevelScore();

        _matcherTrigger.ClearTriggerItems();

        _spawner.DestroyItems();

        _spawner.ClearRandomItems();

        _timer.StopTimer();
    }

    private void Initialize()
    {
        _spawner = new ItemsSpawner();

        _levelStatistic = new LevelStatistic(_matcherTrigger, _gameStatistic.GameStatistic);
    }

    private void OnScoreUp()
    {
        _gameStatistic.UpdateLevelScore();
    }

    private void SetUpLevelPreset()
    {
        float difficulty = _difficultyFormLevel.Evaluate(_gameStatistic.GameStatistic.Level);

        _levelPreset = _levelPresetsConfig.GetRandomLevelPreset((int)difficulty);
    }

    private void SetUpLevel()
    {
        _spawner.SetLevelPreset(_itemsConfig, _levelPreset.ItemsCount, _levelPreset.SpawnOffsets);

        _levelStatistic.SetCountToWin(_levelPreset.ItemsCount);

        _timer.StartTimer(_levelPreset.CountdownTime);

        _spawner.SpawnRandomItems();
    }

    private void OnTimeIsUp()
    {
        _timer.StopTimer();

        _levelMenuView.TimeIsUp();
    }

    private void OnLevelCompleted()
    {
        _matcherTrigger.Sequence.OnComplete(() =>
        {
            _spawner.DestroyItems();
        });

        _spawner.ClearRandomItems();

        _matcherTrigger.ClearTriggerItems();

        _timer.StopTimer();

        _gameStatistic.GameStatistic.LevelUp();

        _gameStatistic.GameStatistic.IncreaseGameScore();

        _levelMenuView.LevelCompleted();

        _gameStatistic.GameStatisticIO.SaveData(_gameStatistic.GameStatistic);
    }
}
