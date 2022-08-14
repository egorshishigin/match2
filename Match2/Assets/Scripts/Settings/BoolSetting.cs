using UnityEngine;

public abstract class BoolSetting : MonoBehaviour
{
    private const int SettingOn = 1;

    private const int SettingOff = 0;

    [SerializeField] private BoolSettingView _settingView;

    [SerializeField] private string _settingName;

    public string SettingName => _settingName;

    public BoolSettingView SettingView => _settingView;

    private void Awake()
    {
        LoadSetting(_settingName);
    }

    private void OnEnable()
    {
        _settingView.SettingChanged += OnSettingChange;
    }

    private void OnDisable()
    {
        _settingView.SettingChanged -= OnSettingChange;
    }

    private void OnSettingChange(bool value)
    {
        SetSettingValue(value);

        SaveSetting(SettingName, value);

        SettingView.ChangeButtonImage(value);
    }

    protected abstract void SetSettingValue(bool value);

    private void SaveSetting(string settingName, bool value)
    {
        if (value)
        {
            PlayerPrefs.SetInt(settingName, SettingOn);
        }
        else
        {
            PlayerPrefs.SetInt(settingName, SettingOff);
        }

        PlayerPrefs.Save();
    }

    private void LoadSetting(string settingName)
    {
        bool settingValue;

        int setting = PlayerPrefs.GetInt(settingName);

        if (setting == 1)
        {
            settingValue = true;
        }
        else
        {
            settingValue = false;
        }

        SetSettingValue(settingValue);

        _settingView.ChangeButtonImage(settingValue);
    }
}
