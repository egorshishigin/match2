using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "LevelPresetsConfig", menuName = "ScriptableObjects/LevelPresetsConfig")]
public class LevelPresetsConfig : ScriptableObject
{
    [SerializeField] private List<LevelPreset> _levelPresets = new List<LevelPreset>();

    public LevelPreset GetRandomLevelPreset(List<LevelPreset> levelPresets)
    {
        int randomPresetIndex = Random.Range(0, levelPresets.Count);

        LevelPreset levelPreset = levelPresets[randomPresetIndex];

        levelPresets.Clear();

        return levelPreset;
    }

    public List<LevelPreset> GetLevelPresetsByDifficulty(int difficulty)
    {
        List<LevelPreset> presetsByDifficulty = new List<LevelPreset>();

        foreach (LevelPreset levelPreset in _levelPresets)
        {
            if (levelPreset.Difficulty == difficulty)
            {
                presetsByDifficulty.Add(levelPreset);
            }
        }

        return presetsByDifficulty;
    }
}
