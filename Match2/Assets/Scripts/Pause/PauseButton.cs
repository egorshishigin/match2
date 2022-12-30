using UnityEngine;
using UnityEngine.UI;

namespace Pause
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;

        [SerializeField] private bool _pauseValue;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(OnPauseClick);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(OnPauseClick);
        }

        private void OnPauseClick()
        {
            Game.Instance.PauseManager.SetPause(_pauseValue);
        }
    }
}