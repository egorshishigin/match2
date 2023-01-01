using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace GameStatistic.View
{
    public class GameStatisticView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;

        [SerializeField] private TMP_Text _level;

        [SerializeField] private Button _homeButton;

        [SerializeField] private Button _shopADButton;

        public Button HomeButton => _homeButton;

        public Button ShopADButton => _shopADButton;

        public void UpdateScoreText(string scoreText)
        {
            _score.text = scoreText;
        }

        public void UpdateLevelText(string levelText)
        {
            _level.text = levelText;
        }
    }
}