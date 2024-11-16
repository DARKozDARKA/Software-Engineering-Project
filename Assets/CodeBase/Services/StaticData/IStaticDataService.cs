using System.Collections.Generic;
using CodeBase.StaticData.Enums;
using CodeBase.StaticData.ScriptableObjects;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        Dictionary<string, LevelData> GetLevels();
        GameData GetGameData();
        PlayerData GetPlayerData(DifficultyData difficultyData);
        DifficultyData GetDifficultyData(Difficulties difficulty);
    }
}