using System.Runtime.InteropServices;

using Newtonsoft.Json;

using Pause;

using Helpers.Config;

using Level.View;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetLanguage();

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern void ExtraStarsRewarded();

    [DllImport("__Internal")]
    private static extern void ShopRewarded();

    [DllImport("__Internal")]
    private static extern void ShowFullScreenAD();

    [SerializeField] private HelpersConfig _config;

    [SerializeField] private string _language = "ru";

    [SerializeField] private LevelMenuView _levelMenu;

    private PauseManager _pauseManager;

    private GameData _gameData;

    public static Game Instance;

    public PauseManager PauseManager => _pauseManager;

    public GameData GameData => _gameData;

    public string Language => _language;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        Initialize();

        LoadExtern();

        SceneManager.LoadScene(1);

        DontDestroyOnLoad(this);
    }

    private void Initialize()
    {
        _language = GetLanguage();

        _pauseManager = new PauseManager();
    }

    public void SaveData()
    {
        string json = JsonConvert.SerializeObject(_gameData);

        SaveExtern(json);
    }

    public void LoadData(string data)
    {
        if(data.Length == 0)
        {
            _gameData = new GameData(0, 0, 0);

            _gameData.Initialize(_config);
        }
        else
        {
            _gameData = JsonConvert.DeserializeObject<GameData>(data);
        }
    }

    public void ShowAD()
    {
        ShowFullScreenAD();
    }

    public void PlayShopAD()
    {
        ShopRewarded();
    }

    public void PlayStarsAD()
    {
        ExtraStarsRewarded();
    }

    public void GiveShopStars()
    {
        _gameData.GiveScore(_config.GetHelpersPrice());

        SaveData();
    }

    public void GiveExtraStars()
    {
        _gameData.GiveExtraStars();

        _levelMenu.ShowLevelScore(_gameData.GameScore.ToString());

        SaveData();
    }
}
