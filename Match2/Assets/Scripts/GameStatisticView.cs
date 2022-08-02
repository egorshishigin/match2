using TMPro;

using UnityEngine;

public class GameStatisticView : MonoBehaviour
{
    [SerializeField] private TMP_Text _level;

    [SerializeField] private TMP_Text _score;

    public void UpdateLevelText(string levelText)
    {
        _level.text = levelText;
    }

    public void UpdateScoreText(string scoreText)
    {
        _score.text = scoreText;
    }
}
