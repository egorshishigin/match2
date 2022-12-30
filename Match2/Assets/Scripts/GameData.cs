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

    private Dictionary<int, int> _helpers = new Dictionary<int, int>();

    public GameData(int level, int score, int gamescore)
    {
        _level = level;

        _score = score;

        _gameScore = gamescore;
    }

    public int Level => _level;

    public int LevelScore => _score;

    public int GameScore => _gameScore;

    public string GetLevelText()
    {
        return string.Format("Level {0}", _level);
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

    public Dictionary<int, int> Helpers => _helpers;

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
        // to-do: fix data load

        //foreach (HelperData helperData in helpersConfig.Helpers)
        //{
        //    _helpers.Add(helperData.ID, 0);
        //}
    }
}
