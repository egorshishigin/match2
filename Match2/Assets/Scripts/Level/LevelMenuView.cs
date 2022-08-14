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

    [SerializeField] private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.localPosition;
    }

    public void LevelCompleted()
    {
        ShowElement(_levelCompletedText, Vector3.zero);

        _nextLevelButton.SetActive(true);

        _restartButton.SetActive(false);
    }

    public void TimeIsUp()
    {
        ShowElement(_timeIsUpText, Vector3.zero);

        _nextLevelButton.SetActive(false);

        _restartButton.SetActive(true);
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
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOLocalMove(position, _animationDuration));

        sequence.OnComplete(() =>
        {
            sequence.Kill();
        });
    }

    private void SetText(string text)
    {
        _messageText.text = text;
    }
}
