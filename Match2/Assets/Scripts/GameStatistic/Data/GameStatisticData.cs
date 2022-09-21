using System;

namespace GameStatistic
{
    [Serializable]
    public class GameStatisticData
    {
        private int _level;

        private int _score;

        private int _gameScore;

        public GameStatisticData()
        {
            _level = 0;

            _score = 0;

            _gameScore = 0;
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
    }
}