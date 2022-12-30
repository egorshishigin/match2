using System;

using DG.Tweening;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Level.View
{
    public class LevelMenuView : MonoBehaviour
    {
        [SerializeField] private float _animationDuration;

        [SerializeField] private TMP_Text _messageText;

        [SerializeField] private TMP_Text _levelScore;

        [SerializeField] private string _timeIsUpText;

        [SerializeField] private string _pauseText;

        [SerializeField] private Button _restartButton;

        [SerializeField] private Button _nextLevelButton;

        [SerializeField] private Button _startGameButton;

        [SerializeField] private Button _pauseButton;

        [SerializeField] private Button _resumeButton;

        [SerializeField] private Vector3 _startPosition;

        public event Action PauseClicked = delegate { };

        public Button RestartButton => _restartButton;

        public Button NextLevelButton => _nextLevelButton;

        public Button StartGameButton => _startGameButton;

        public Button ResumeButton => _resumeButton;

        private void Start()
        {
            _startPosition = transform.localPosition;
        }

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(ShowGamePauseMenu);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(ShowGamePauseMenu);
        }

        public void ShowGamePauseMenu()
        {
            ShowElement(_pauseText, Vector3.zero);

            _nextLevelButton.gameObject.SetActive(false);

            _restartButton.gameObject.SetActive(true);

            _resumeButton.gameObject.SetActive(true);

            _levelScore.gameObject.SetActive(false);

            PauseClicked.Invoke();
        }

        public void LevelCompleted(int level)
        {
            ShowElement($"Level {level} completed", Vector3.zero);

            _nextLevelButton.gameObject.SetActive(true);

            _restartButton.gameObject.SetActive(false);

            _resumeButton.gameObject.SetActive(false);

            _pauseButton.gameObject.SetActive(false);

            _levelScore.gameObject.SetActive(true);
        }

        public void ShowLevelScore(string levelScore)
        {
            _levelScore.text = levelScore;
        }

        public void TimeIsUp()
        {
            ShowElement(_timeIsUpText, Vector3.zero);

            _nextLevelButton.gameObject.SetActive(false);

            _resumeButton.gameObject.SetActive(false);

            _restartButton.gameObject.SetActive(true);

            _pauseButton.gameObject.SetActive(false);

            _levelScore.gameObject.SetActive(false);
        }

        public void HideElement()
        {
            AnimateElement(_startPosition);
        }

        private void ShowElement(string text, Vector3 position)
        {
            AnimateElement(position);

            SetText(text);
        }

        private void AnimateElement(Vector3 position)
        {
            Tween tween = transform.DOLocalMove(position, _animationDuration).SetUpdate(true);

            tween.OnComplete(() =>
            {
                tween.Kill();
                
                Debug.Log("paused");
            });
        }

        private void SetText(string text)
        {
            _messageText.text = text;
        }
    }
}