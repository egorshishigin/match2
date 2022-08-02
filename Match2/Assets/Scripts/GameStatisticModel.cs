using System;

[Serializable]
public class GameStatisticModel
{
    private int _level;

    private int _score;

    public GameStatisticModel() { }

    public int Level => _level;

    public int Score => _score;

    public void LevelUp()
    {
        _level++;
    }
}
