using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class HelperView : MonoBehaviour
    {
        [SerializeField] private string _name;

        [SerializeField] TMP_Text _count;

        [SerializeField] private Button _helperButton;

        public string Name => _name;

        public event Action<string> ButtonClicked = delegate { };

        private void OnEnable()
        {
            _helperButton.onClick.AddListener(ButtonClick);
        }

        private void OnDisable()
        {
            _helperButton.onClick.RemoveListener(ButtonClick);
        }

        public void UpdateCountText(int count)
        {
            _count.text = count.ToString();
        }

        private void ButtonClick()
        {
            ButtonClicked.Invoke(_name);
        }
    }
}