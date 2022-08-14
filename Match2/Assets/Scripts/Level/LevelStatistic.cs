using System;

using UnityEngine;

public class LevelStatistic : IDisposable
{
    private int _itemsCountToWin;

    private int _currentMatchedItems;

    private ItemsMatcherTrigger _matcherTrigger;

    private GameStatistic _gameStatistic;

    public event Action ScoreUp = delegate { };

    public LevelStatistic(ItemsMatcherTrigger matcherTrigger, GameStatistic gameStatistic)
    {
        _matcherTrigger = matcherTrigger;

        _gameStatistic = gameStatistic;

        Initialize();
    }

    public event Action LevelCompleted = delegate { };

    void IDisposable.Dispose()
    {
        _matcherTrigger.ItemsMatch.RemoveListener(OnItemsMatch);
    }

    public void SetCountToWin(int countToWin)
    {
        _currentMatchedItems = 0;

        _itemsCountToWin = countToWin;
    }

    private void Initialize()
    {
        _matcherTrigger.ItemsMatch.AddListener(OnItemsMatch);
    }

    private void OnItemsMatch()
    {
        _gameStatistic.ScoreUp(5);

        ScoreUp.Invoke();

        _currentMatchedItems++;

        if (_currentMatchedItems == _itemsCountToWin)
        {
            LevelCompleted.Invoke();
        }
    }
}
