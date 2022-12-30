using System.IO;

using Pause;

using Helpers.Config;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private HelpersConfig _config;

    private PauseManager _pauseManager;

    private GameData _gameData;

    public static Game Instance;

    public PauseManager PauseManager => _pauseManager;

    public GameData GameData => _gameData;

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

        LoadData();

        SceneManager.LoadScene(1);

        DontDestroyOnLoad(this);
    }

    private void Initialize()
    {
        _pauseManager = new PauseManager();
    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(_gameData);

        Debug.Log($"Save data = {jsonData}");

        File.WriteAllText(Application.persistentDataPath + "/GameData.json", jsonData);
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/GameData.json"))
        {
            string jsonData = File.ReadAllText(Application.persistentDataPath + "/GameData.json");

            if (jsonData.Length == 0)
            {
                _gameData = new GameData(0, 0, 0);

                _gameData.Initialize(_config);
            }
            else
            {
                _gameData = JsonUtility.FromJson<GameData>(jsonData);

                _gameData.Initialize(_config);
            }
        }
        else
        {
            _gameData = new GameData(0, 0, 0);

            _gameData.Initialize(_config);
        }

    }
}
