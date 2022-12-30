using System.Collections.Generic;

using GameStatistic;

using Level.Config;

using UnityEngine;

namespace Level.Configurator
{
    public class LevelConfigurator
    {
        private LevelPresetsConfig _levelPresetsConfig;

        private LevelPreset _levelPreset;

        private GameData _gameData;

        private AnimationCurve _difficultyFormLevel;

        public LevelConfigurator(LevelPresetsConfig levelPresetsConfig, GameData gameData, AnimationCurve difficultyFormLevel)
        {
            _levelPresetsConfig = levelPresetsConfig;

            _gameData = gameData;

            _difficultyFormLevel = difficultyFormLevel;
        }

        public LevelPreset SetUpLevelPreset()
        {
            float difficulty = _difficultyFormLevel.Evaluate(_gameData.Level);

            List<LevelPreset> levelPresets = _levelPresetsConfig.GetLevelPresetsByDifficulty((int)difficulty);

            _levelPreset = _levelPresetsConfig.GetRandomLevelPreset(levelPresets);

            return _levelPreset;
        }
    }
}