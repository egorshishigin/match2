using TMPro;

using UnityEngine;

public class GameStatisticView : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;

    [SerializeField] private TMP_Text _level;

    public void UpdateScoreText(string scoreText)
    {
        _score.text = scoreText;
    }

    public void UpdateLevelText(string levelText)
    {
        _level.text = levelText;
    }
}
