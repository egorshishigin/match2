using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "LevelPresetsConfig", menuName = "ScriptableObjects/LevelPresetsConfig")]
public class LevelPresetsConfig : ScriptableObject
{
    [SerializeField] private List<LevelPreset> _levelPresets = new List<LevelPreset>();

    public LevelPreset GetRandomLevelPreset()
    {
        int randomPresetIndex = Random.Range(0, _levelPresets.Count);

        LevelPreset levelPreset = _levelPresets[randomPresetIndex];

        return levelPreset;
    }
}
