using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private float _duration;

    [SerializeField] private GameObject _pointerIcon;

    [SerializeField] private Transform _matcherTrigger;

    [SerializeField] private GameBootstrap _bootstrap;

    [SerializeField] private GameObject _rootObject;

    private Tween _tween;

    private void Start()
    {
        _bootstrap.LevelModel.LevelStarted += ShowTutorial;
    }

    private void OnDisable()
    {
        _bootstrap.LevelModel.LevelStarted -= ShowTutorial;
    }

    public void ShowTutorial()
    {
        int index = Random.Range(0, _bootstrap.ItemsSpawner.SpawnedItems.Count);

        var itemTransform = _bootstrap.ItemsSpawner.SpawnedItems[index].transform;

        _pointerIcon.transform.position = itemTransform.localPosition;

        if (Game.Instance.GameData.Level == 0)
        {
            Game.Instance.PauseManager.SetPause(true);

            AnimatePointerIcon();
        }
        else
        {
            TurnOffTutorial();
        }
    }

    private void AnimatePointerIcon()
    {
        _tween = _pointerIcon.transform.DOLocalMove(_matcherTrigger.position, _duration).SetLoops(-1).SetUpdate(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TurnOffTutorial();
    }

    private void TurnOffTutorial()
    {
        _tween.Kill();

        Destroy(_rootObject);

        Game.Instance.PauseManager.SetPause(false);
    }
}
