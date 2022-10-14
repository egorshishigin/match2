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

using Helpers;
using Helpers.Shop.Model;
using Helpers.Shop.View;
using Helpers.Shop.Controller;
using Helpers.Config;
using Helpers.Inventory;

using Booster;

using Timer;

using PlayerInput;

using UnityEngine;
using UnityEngine.UI;


public class GameBootstrap : MonoBehaviour
{
    [Header("Items match")]

    [SerializeField] private ItemsMatcherTrigger _matcherTrigger;

    [SerializeField] private ItemsConfig _itemsConfig;

    [Header("Game")]

    [SerializeField] private LevelPresetsConfig _levelPresetsConfig;

    [SerializeField] private AnimationCurve _difficultyFormLevel;

    [SerializeField] private CountdownTimer _timer;

    [SerializeField] private ScoreBooster _scoreBooster;

    [SerializeField] private RigibodyMouseDrag _playerControl;

    [Header("Views")]

    [SerializeField] private LevelMenuView _levelMenuView;

    [SerializeField] private GameStatisticView _gameStatisticView;

    [SerializeField] private HelperView _itemsShakerView;

    [SerializeField] private HelperView _extraTimeHelperView;

    [SerializeField] private ShopItemView _shopItemViewTemplate;

    [SerializeField] private Button _openShopButton;

    [SerializeField] private Transform _shopItemsHolder;

    [Header("Helpers")]

    [SerializeField] private HelpersConfig _config;

    [SerializeField] private float _itemsShakeForce;

    [SerializeField] private Transform _forcePoint;

    [SerializeField] private float _forceRadius;

    [SerializeField] private float _extraTimeAmount;

    private ItemsSpawner _spawner;

    private LevelModel _level;

    private LevelConfigurator _levelConfigurator;

    private LevelEventsHandler _levelEventsHandler;

    private GameStatisticData _gameStatistic;

    private GameStatisticIO _gameStatisticIO;

    private GameStatisticController _statisticController;

    private LevelMenuController _levelMenuController;

    private ShopModel _shopModel;

    private ShopController _shopController;

    private InventoryData _inventoryData;

    private InventoryDataIO _inventoryDataIO;

    private ItemsShaker _itemsShaker;

    private ExtraTimeHelper _extraTimeHelper;

    private void Start()
    {
        SetApplicationFrameRate();

        LoadGameStatisticData();

        CreateItemsSpawner();

        CreateLevel();

        CreateShop();

        CreateStatisticController();

        CreateItemsShaker();

        CreateExtraTimeHelper();
    }

    private void CreateItemsShaker()
    {
        _itemsShaker = new ItemsShaker(_inventoryData, _inventoryDataIO, _itemsShakerView, _shopModel, _itemsShakeForce, _forcePoint, _forceRadius);
    }

    private void CreateExtraTimeHelper()
    {
        _extraTimeHelper = new ExtraTimeHelper(_inventoryData, _inventoryDataIO, _extraTimeHelperView, _shopModel, _timer, _extraTimeAmount);
    }

    private void CreateShop()
    {
        _inventoryDataIO = new InventoryDataIO(_config);

        _inventoryData = _inventoryDataIO.LoadData();

        _shopModel = new ShopModel(_inventoryData, _gameStatistic, _config);

        _shopController = new ShopController(_config, _shopItemViewTemplate, _openShopButton, _shopItemsHolder, _shopModel, _inventoryData, _inventoryDataIO);
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
        _statisticController = new GameStatisticController(_gameStatisticView, _gameStatistic, _level, _gameStatisticIO, _shopModel);
    }

    private void CreateItemsSpawner()
    {
        _spawner = new ItemsSpawner(_itemsConfig);
    }

    private void SetApplicationFrameRate()
    {
        Application.targetFrameRate = 60;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(_forcePoint.position, _forceRadius);
    }
}
