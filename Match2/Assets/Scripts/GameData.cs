using System;
using System.Collections.Generic;

using Helpers.Config;

using UnityEngine;

[Serializable]
public class GameData
{
    [SerializeField] private int _level;

    [SerializeField] private int _score;

    [SerializeField] private int _gameScore;

    [SerializeField] private string _inventory;

    [SerializeField] private Dictionary<int, int> _helpers = new Dictionary<int, int>();

    public GameData(int level, int score, int gamescore)
    {
        _level = level;

        _score = score;

        _gameScore = gamescore;

        _helpers = new Dictionary<int, int>();
    }

    public int Level => _level;

    public int LevelScore => _score;

    public int GameScore => _gameScore;

    public Dictionary<int, int> Helpers => _helpers;

    public event Action ShopADWatched = delegate { };

    public string GetLevelText()
    {
        if (Game.Instance.Language == "ru")
        {
            return string.Format("Уровень {0}", _level);
        }
        else
        {
            return string.Format("Level {0}", _level);
        }
    }

    public void GiveExtraStars()
    {
        _gameScore += _score * 2;
    }

    public void LevelUp()
    {
        _level++;
    }

    public void ScoreUp(int ratio)
    {
        _score += 1 * ratio;
    }

    public void ResetLevelScore()
    {
        _score = 0;
    }

    public void IncreaseGameScore()
    {
        _gameScore += _score;
    }

    public void SpendScore(int amount)
    {
        _gameScore -= amount;
    }

    public void GiveScore(int amount)
    {
        _gameScore += amount;

        ShopADWatched.Invoke();
    }

    public void ChangeHelperCount(int id, int count)
    {
        _helpers[id] += count;
    }

    public int GetHelperCount(int id)
    {
        int count = _helpers[id];

        return count;
    }

    public void Initialize(HelpersConfig helpersConfig)
    {
        _helpers = new Dictionary<int, int>();

        foreach (HelperData helperData in helpersConfig.Helpers)
        {
            _helpers.Add(helperData.ID, 0);
        }
    }
}
