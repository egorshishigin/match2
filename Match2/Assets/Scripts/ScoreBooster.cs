using System.Collections;

using UnityEngine;

public class ScoreBooster : MonoBehaviour
{
    [SerializeField] private ItemsMatcherTrigger _matcherTrigger;

    [SerializeField] private float _boostCountdownTime;

    [SerializeField] private ScoreBoosterView _boosterView;

    private int _boostAmount;

    private float _countDownTime;

    public int ScoreBoostAmount => _boostAmount;

    private void Start()
    {
        _countDownTime = _boostCountdownTime;

        _boosterView.BoosterSlider.maxValue = _boostCountdownTime;
    }

    private void OnEnable()
    {
        _matcherTrigger.ItemsMatch.AddListener(OnItemsMatch);
    }

    private void OnDisable()
    {
        _matcherTrigger.ItemsMatch.RemoveListener(OnItemsMatch);
    }

    public void ResetBooster()
    {
        StopAllCoroutines();

        _countDownTime = 0;

        _boostAmount = 1;

        _boosterView.UpdateSliderValue(_countDownTime);

        _boosterView.UpdateBoosterText(_boostAmount);
    }

    private void OnItemsMatch()
    {
        StartCoroutine(BoosterTimer());
    }

    private IEnumerator BoosterTimer()
    {
        _boostAmount++;

        _countDownTime = _boostCountdownTime;

        _boosterView.UpdateBoosterText(_boostAmount);

        while (_countDownTime > 0)
        {
            _countDownTime--;

            _boosterView.UpdateSliderValue(_countDownTime);

            yield return null;
        }

        _boostAmount--;

        _countDownTime = 0;

        _boosterView.UpdateBoosterText(_boostAmount);
    }
}
