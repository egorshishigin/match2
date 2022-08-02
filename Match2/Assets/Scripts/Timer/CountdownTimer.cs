using System;

using TMPro;

using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

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

    private void TimerTick()
    {
        if (_timerRunning)
        {
            if (_remaningTime > 0)
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
}
