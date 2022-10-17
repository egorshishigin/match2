using System;

using TMPro;

using DG.Tweening;

using UnityEngine;

namespace Timer
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;

        [SerializeField] private TMP_Text _extraTimeText;

        [SerializeField] private float _textFadeDuration;

        private float _remaningTime;

        private bool _timerRunning;

        public event Action TimeIsUp = delegate { };

        private void Update()
        {
            TimerTick();
        }

        public void StartTimer(float countdownTime)
        {
            _timerRunning = true;

            _remaningTime = countdownTime;
        }

        public void StopTimer()
        {
            _timerRunning = false;
        }

        public void PlayTimer()
        {
            _timerRunning = true;
        }

        public void GiveExtraTime(float amount)
        {
            if (_timerRunning && _remaningTime > 1)
            {
                _remaningTime += amount;

                AnimateExtraTimeText(amount);
            }
        }

        private void TimerTick()
        {
            if (_timerRunning)
            {
                if (_remaningTime > 1)
                {
                    _remaningTime -= Time.deltaTime;
                }
                else
                {
                    TimeIsUp.Invoke();

                    _remaningTime = 0;

                    _timerRunning = false;
                }

                DisplayTime(_remaningTime);
            }
        }

        private void DisplayTime(float time)
        {
            float minutes = Mathf.FloorToInt(time / 60);

            float seconds = Mathf.FloorToInt(time % 60);

            _timerText.text = $"{minutes:00}:{seconds:00}";
        }

        private void AnimateExtraTimeText(float amount)
        {
            _extraTimeText.gameObject.SetActive(true);

            _extraTimeText.text = $"+ {amount} s";

            Sequence textFade = DOTween.Sequence();

            textFade.Append(_extraTimeText.DOFade(0, _textFadeDuration));

            textFade.OnComplete(() =>
            {
                textFade.Kill();

                _extraTimeText.DOFade(1, 0);

                _extraTimeText.gameObject.SetActive(false);
            });
        }
    }
}