using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Settings.View
{
    public class BoolSettingView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _buttonImage;

        [SerializeField] private Sprite _isOnImage;

        [SerializeField] private Sprite _isOffImage;

        private bool _isOn;

        public event Action<bool> SettingChanged = delegate { };

        public void OnPointerClick(PointerEventData eventData)
        {
            SettingChanged.Invoke(_isOn);
        }

        public void ChangeButtonImage(bool value)
        {
            if (value)
            {
                _isOn = false;

                _buttonImage.sprite = _isOffImage;
            }
            else
            {
                _isOn = true;

                _buttonImage.sprite = _isOnImage;
            }
        }
    }
}