using UnityEngine;

namespace Settings.Sound
{
    public class SoundSetting : BoolSetting
    {
        protected override void SetSettingValue(bool value)
        {
            AudioListener.pause = value;
        }
    }
}