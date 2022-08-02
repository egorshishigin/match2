using DG.Tweening;

using TMPro;

using UnityEngine;

public class LevelMenuView : MonoBehaviour
{
    [SerializeField] private float _animationDuration;

    [SerializeField] private TMP_Text _messageText;

    [SerializeField] private string _levelCompletedText;

    [SerializeField] private string _timeIsUpText;

    [SerializeField] private GameObject _restartButton;

    [SerializeField] private GameObject _nextLevelButton;

    public void LevelCompleted()
    {
        ShowElement(_levelCompletedText);

        _nextLevelButton.SetActive(true);

        _restartButton.SetActive(false);
    }

    public void TimeIsUp()
    {
        ShowElement(_timeIsUpText);

        _nextLevelButton.SetActive(false);

        _restartButton.SetActive(true);
    }

    private void ShowElement(string text)
    {
        AnimateElement();

        SetText(text);
    }

    private void AnimateElement()
    {
        gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOLocalMove(Vector3.zero, _animationDuration));
    }

    private void SetText(string text)
    {
        _messageText.text = text;
    }
}
