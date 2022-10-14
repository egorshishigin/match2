using System;
using System.Collections.Generic;

using Helpers.Config;

namespace Helpers.Inventory
{
    [Serializable]
    public class InventoryData
    {
        private Dictionary<string, int> _helpers = new Dictionary<string, int>();

        [NonSerialized]
        private HelpersConfig _config;

        public InventoryData(HelpersConfig config)
        {
            _config = config;

            Initialize();
        }

        public Dictionary<string, int> Helpers => _helpers;

        public void ChangeHelperCount(string name, int count)
        {
            _helpers[name] += count;
        }

        public int GetHelperCount(string name)
        {
            int count = _helpers[name];

            return count;
        }

        private void Initialize()
        {
            foreach (HelperData helperData in _config.Helpers)
            {
                _helpers.Add(helperData.Name, 0);
            }
        }
    }
}