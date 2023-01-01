using TMPro;

using UnityEngine;

namespace Localization
{
    public class TranslateText : MonoBehaviour
    {
        [SerializeField] private string _ruText;

        [SerializeField] private string _enText;

        private TMP_Text _text;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();

            if (Game.Instance.Language == "ru")
            {
                _text.text = _ruText;
            }
            else
            {
                _text.text = _enText;
            }
        }
    }
}