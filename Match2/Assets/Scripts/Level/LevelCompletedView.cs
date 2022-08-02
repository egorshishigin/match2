using DG.Tweening;

using TMPro;

using UnityEngine;

public class LevelCompletedView : MonoBehaviour
{
    [SerializeField] private float _duration;

    [SerializeField] private TMP_Text _messageText;

    public void ShowElement(string text)
    {
        AnimateElement();

        SetText(text);
    }

    private void AnimateElement()
    {
        gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOLocalMove(Vector3.zero, _duration));
    }

    private void SetText(string text)
    {
        _messageText.text = text;
    }
}
