using UnityEngine;

public class GameStatisticController : MonoBehaviour
{
    [SerializeField] private GameStatisticView _statisticView;

    private GameStatistic _gameStatistic;

    private GameStatisticIO _statisticIO;

    public GameStatistic GameStatistic => _gameStatistic;

    public GameStatisticIO GameStatisticIO => _statisticIO;

    private void Start()
    {
        Application.targetFrameRate = 60;

        _statisticIO = new GameStatisticIO();

        _gameStatistic = _statisticIO.LoadData();

        UpdateGameScore();

        UpdateLevelText();
    }

    public void UpdateGameScore()
    {
        _statisticView.UpdateScoreText(_gameStatistic.GameScore.ToString());
    }

    public void UpdateLevelScore()
    {
        _statisticView.UpdateScoreText(_gameStatistic.LevelScore.ToString());
    }

    public void UpdateLevelText()
    {
        _statisticView.UpdateLevelText(_gameStatistic.GetLevelText());
    }
}
