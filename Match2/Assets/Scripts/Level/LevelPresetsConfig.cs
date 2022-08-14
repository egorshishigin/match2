using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "LevelPresetsConfig", menuName = "ScriptableObjects/LevelPresetsConfig")]
public class LevelPresetsConfig : ScriptableObject
{
    [SerializeField] private List<LevelPreset> _levelPresets = new List<LevelPreset>();

    private List<LevelPreset> _presetsByDifficulty;

    public LevelPreset GetRandomLevelPreset(int difficulty)
    {
        GetLevelPresetsByDifficulty(difficulty);

        int randomPresetIndex = Random.Range(0, _presetsByDifficulty.Count);

        LevelPreset levelPreset = _presetsByDifficulty[randomPresetIndex];

        _presetsByDifficulty.Clear();

        return levelPreset;
    }

    private void GetLevelPresetsByDifficulty(int difficulty)
    {
        foreach (LevelPreset levelPreset in _levelPresets)
        {
            if (levelPreset.Difficulty == difficulty)
            {
                _presetsByDifficulty.Add(levelPreset);
            }
        }
    }
}
