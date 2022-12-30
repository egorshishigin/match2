using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class HelperView : MonoBehaviour
    {
        [SerializeField] private int _id;

        [SerializeField] TMP_Text _count;

        [SerializeField] private Button _helperButton;

        public int ID => _id;

        public event Action<int> ButtonClicked = delegate { };

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
            ButtonClicked.Invoke(_id);
        }
    }
}