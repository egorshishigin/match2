using System.Linq;
using System.Collections.Generic;

using UnityEngine;

namespace Helpers.Config
{
    [CreateAssetMenu(fileName = "HelpersConfig", menuName = "ScriptableObjects/HelpersConfig")]
    public class HelpersConfig : ScriptableObject
    {
        [SerializeField] private List<HelperData> _helperDatas = new List<HelperData>();

        public List<HelperData> Helpers => _helperDatas;

        public HelperData GetHelperByName(string name)
        {
            HelperData helperData = _helperDatas.FirstOrDefault(n => n.Name == name);

            return helperData;
        }
    }
}