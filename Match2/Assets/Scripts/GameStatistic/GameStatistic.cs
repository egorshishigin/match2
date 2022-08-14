using System;

[Serializable]
public class GameStatistic
{
    private int _level;

    private int _score;

    private int _gameScore;

    public GameStatistic() { }

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

    public void ScoreUp(int amout)
    {
        _score += amout;
    }

    public void ResetLevelScore()
    {
        _score = 0;
    }

    public void IncreaseGameScore()
    {
        _gameScore += _score;
    }
}
