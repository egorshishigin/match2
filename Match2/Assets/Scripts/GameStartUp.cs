using UnityEngine;

public class GameStartUp : MonoBehaviour
{
    [SerializeField] private GameStatisticView _statisticView;

    private GameStatisticModel _statisticModel;

    private GameStatisticIO _statisticIO;

    public GameStatisticModel GameStatisticModel => _statisticModel;

    public GameStatisticIO GameStatisticIO => _statisticIO;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        _statisticIO = new GameStatisticIO();

        _statisticModel = _statisticIO.LoadData();

        UpdateStatistic();
    }

    public void UpdateStatistic()
    {
        _statisticView.UpdateLevelText(_statisticModel.Level.ToString());
    }
}
