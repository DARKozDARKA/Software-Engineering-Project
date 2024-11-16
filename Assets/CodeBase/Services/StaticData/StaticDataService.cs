using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Enums;
using CodeBase.StaticData.ScriptableObjects;
using CodeBase.StaticData.Strings;
using CodeBase.Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<string, LevelData> _levels = new();
        private Dictionary<Difficulties, DifficultyData> _difficulties = new();
        private GameData _gameData;
        private PlayerData _playerData;

        public void Load()
        {
            _levels = LoadResources<LevelData>(StaticDataPath.LevelsData)
                .ToDictionary(_ => _.SceneName, _ => _);
            
            _difficulties = LoadResources<DifficultyData>(StaticDataPath.DifficultiesPath)
                .ToDictionary(_ => _.DifficultyType, _ => _);

            _gameData = LoadResource<GameData>(StaticDataPath.GameData);
            _playerData = LoadResource<PlayerData>(StaticDataPath.PlayerData);
        }
        
        public Dictionary<string, LevelData> GetLevels() =>
            _levels;

        public DifficultyData GetDifficultyData(Difficulties difficulty) =>
            _difficulties[difficulty];

        public GameData GetGameData() =>
            _gameData;

        public PlayerData GetPlayerData(DifficultyData difficultyData)
        {
            PlayerData newPlayerData = ScriptableObject
                .CreateInstance<PlayerData>()
                .With(_ => _.Copy(_playerData));
            
            newPlayerData.HorizontalSpeed *= difficultyData.PlayerSpeedModifier;
            return newPlayerData;
        }
        
        private T LoadResource<T>(string path) where T : Object =>
            Resources.Load<T>(path);

        private T[] LoadResources<T>(string path) where T : Object =>
            Resources.LoadAll<T>(path);
    }
}