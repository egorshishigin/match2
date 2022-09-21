using UnityEngine;

namespace Settings.Vibration
{
    public class VibrationSetting : BoolSetting
    {
        [SerializeField] private Vibrator _vibrator;

        protected override void SetSettingValue(bool value)
        {
            _vibrator.isOn = !value;
        }
    }
}