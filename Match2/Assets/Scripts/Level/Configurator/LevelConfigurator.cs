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

        private GameStatisticData _gameStatistic;

        private AnimationCurve _difficultyFormLevel;

        public LevelConfigurator(LevelPresetsConfig levelPresetsConfig, GameStatisticData gameStatistic, AnimationCurve difficultyFormLevel)
        {
            _levelPresetsConfig = levelPresetsConfig;

            _gameStatistic = gameStatistic;

            _difficultyFormLevel = difficultyFormLevel;
        }

        public LevelPreset SetUpLevelPreset()
        {
            float difficulty = _difficultyFormLevel.Evaluate(_gameStatistic.Level);

            List<LevelPreset> levelPresets = _levelPresetsConfig.GetLevelPresetsByDifficulty((int)difficulty);

            _levelPreset = _levelPresetsConfig.GetRandomLevelPreset(levelPresets);

            return _levelPreset;
        }
    }
}