using System.Collections.Generic;

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

    [SerializeField] private ScoreBooster _scoreBooster;

    [SerializeField] private TutorialAnimation _tutorialAnimation;

    [SerializeField] private RigibodyMouseDrag _playerControl;

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

        ShowTutorial();
    }

    public void NextLevel()
    {
        _playerControl.enabled = true;

        _gameStatistic.GameStatistic.ResetLevelScore();

        _gameStatistic.UpdateLevelScore();

        _spawner.DestroyItems();

        _scoreBooster.ResetBooster();

        SetUpLevelPreset();

        SetUpLevel();
    }

    public void RestartLevel()
    {
        _playerControl.enabled = true;

        _matcherTrigger.ClearTriggerItems();

        _scoreBooster.ResetBooster();

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

        _levelStatistic = new LevelStatistic(_matcherTrigger);
    }

    private void ShowTutorial()
    {
        if (_gameStatistic.GameStatistic.Level == 0)
        {
            List<Vector3> points = new List<Vector3>();

            Vector3 offset = new Vector3(0, 2, 0);

            points.Add(_spawner.Items[0].transform.position + offset);

            points.Add(_matcherTrigger.transform.position + offset);

            _tutorialAnimation.AnimatePointer(points.ToArray());

            points.Clear();
        }
        else return;
    }

    private void OnScoreUp()
    {
        _gameStatistic.GameStatistic.ScoreUp(_scoreBooster.ScoreBoostAmount);

        _gameStatistic.UpdateLevelScore();
    }

    private void SetUpLevelPreset()
    {
        float difficulty = _difficultyFormLevel.Evaluate(_gameStatistic.GameStatistic.Level);

        List<LevelPreset> levelPresets = _levelPresetsConfig.GetLevelPresetsByDifficulty((int)difficulty);

        _levelPreset = _levelPresetsConfig.GetRandomLevelPreset(levelPresets);
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
        _playerControl.enabled = false;

        _timer.StopTimer();

        _levelMenuView.TimeIsUp();
    }

    private void OnLevelCompleted()
    {
        _playerControl.enabled = false;

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
