using GameStatistic;
using GameStatistic.Controller;
using GameStatistic.IO;
using GameStatistic.View;

using Items.Config;
using Items.MatcherTrigger;
using Items.Spawner;

using Level.Config;
using Level.Configurator;
using Level.Controller;
using Level.View;
using Level.Model;
using Level.EventsHandler;

using Booster;

using Timer;

using PlayerInput;

using UnityEngine;


public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private ItemsMatcherTrigger _matcherTrigger;

    [SerializeField] private ItemsConfig _itemsConfig;

    [SerializeField] private LevelPresetsConfig _levelPresetsConfig;

    [SerializeField] private AnimationCurve _difficultyFormLevel;

    [SerializeField] private LevelMenuView _levelMenuView;

    [SerializeField] private GameStatisticView _gameStatisticView;

    [SerializeField] private CountdownTimer _timer;

    [SerializeField] private ScoreBooster _scoreBooster;

    [SerializeField] private RigibodyMouseDrag _playerControl;

    private ItemsSpawner _spawner;

    private LevelModel _level;

    private LevelConfigurator _levelConfigurator;

    private LevelEventsHandler _levelEventsHandler;

    private GameStatisticData _gameStatistic;

    private GameStatisticIO _gameStatisticIO;

    private GameStatisticController _statisticController;

    private LevelMenuController _levelMenuController;

    private void Start()
    {
        SetApplicationFrameRate();

        LoadGameStatisticData();

        CreateItemsSpawner();

        CreateLevel();

        CreateStatisticController();
    }

    private void CreateLevel()
    {
        _levelConfigurator = new LevelConfigurator(_levelPresetsConfig, _gameStatistic, _difficultyFormLevel);

        _level = new LevelModel(_matcherTrigger, _spawner, _levelConfigurator, _timer);

        _levelEventsHandler = new LevelEventsHandler(_level, _gameStatistic, _scoreBooster, _levelMenuView, _playerControl);

        _levelMenuController = new LevelMenuController(_levelMenuView, _level, _playerControl);
    }

    private void LoadGameStatisticData()
    {
        _gameStatisticIO = new GameStatisticIO();

        _gameStatistic = _gameStatisticIO.LoadData();
    }

    private void CreateStatisticController()
    {
        _statisticController = new GameStatisticController(_gameStatisticView, _gameStatistic, _level, _gameStatisticIO);
    }

    private void CreateItemsSpawner()
    {
        _spawner = new ItemsSpawner(_itemsConfig);
    }

    private void SetApplicationFrameRate()
    {
        Application.targetFrameRate = 60;
    }
}
