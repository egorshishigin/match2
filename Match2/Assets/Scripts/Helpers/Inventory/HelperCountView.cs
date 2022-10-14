using TMPro;

using UnityEngine;

namespace Helpers.Inventory.View
{
    public class HelperCountView : MonoBehaviour
    {
        [SerializeField] TMP_Text _count;

        public void UpdateCountText(string count)
        {
            _count.text = count;
        }
    }
}