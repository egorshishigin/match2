using System;

public class LevelStatistic : IDisposable
{
    private int _itemsCountToWin;

    private int _currentMatchedItems;

    private ItemsMatcherTrigger _matcherTrigger;

    public event Action ScoreUp = delegate { };

    public LevelStatistic(ItemsMatcherTrigger matcherTrigger)
    {
        _matcherTrigger = matcherTrigger;

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
        ScoreUp.Invoke();

        _currentMatchedItems++;

        if (_currentMatchedItems == _itemsCountToWin)
        {
            LevelCompleted.Invoke();
        }
    }
}
