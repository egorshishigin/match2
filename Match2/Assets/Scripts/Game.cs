using System.Collections;
using System.Runtime.InteropServices;

using Newtonsoft.Json;

using Pause;

using Helpers.Config;

using TMPro;

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

    [SerializeField] private float _loadDelay;

    private PauseManager _pauseManager;

    private GameData _gameData;

    public static Game Instance;

    public PauseManager PauseManager => _pauseManager;

    public GameData GameData => _gameData;

    public string Language => _language;

    private void Awake()
    {
        SetApplicationFrameRate();

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

        StartCoroutine(LoadLevel(1));

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
        if (data.Length == 0)
        {
            _gameData = new GameData(0, 0, 0);
        }
        else
        {
            _gameData = JsonConvert.DeserializeObject<GameData>(data);
        }

        _gameData.Initialize(_config);
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

        SaveData();
    }

    private IEnumerator LoadLevel(int id)
    {
        yield return new WaitForSeconds(_loadDelay);

        SceneManager.LoadScene(id);

        ShowFullScreenAD();
    }

    private void SetApplicationFrameRate()
    {
        Application.targetFrameRate = 60;
    }
}
