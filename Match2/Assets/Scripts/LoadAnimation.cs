using DG.Tweening;

using UnityEngine;

public class LoadAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _angle;

    [SerializeField] private float _duration;

    [SerializeField] private RotateMode _rotateMode;

    private Tween _tween;

    private void Start()
    {
        _tween = transform.DOLocalRotate(_angle, _duration, _rotateMode).SetLoops(-1);
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }
}
