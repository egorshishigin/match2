using DG.Tweening;

using UnityEngine;

public class TutorialAnimation : MonoBehaviour
{
    [SerializeField] private Transform _pointer;

    [SerializeField] private float _duration;

    private Sequence _sequence;

    public void AnimatePointer(Vector3[] points)
    {
        _pointer.gameObject.SetActive(true);

        _pointer.transform.position = points[0];

        _sequence = DOTween.Sequence();

        _sequence.Append(_pointer.DOLocalPath(points, _duration, PathType.Linear, PathMode.Sidescroller2D)).SetLoops(2, LoopType.Yoyo);

        _sequence.OnComplete(() =>
        {
            _pointer.gameObject.SetActive(false);

            _sequence.Kill();
        });
    }
}
